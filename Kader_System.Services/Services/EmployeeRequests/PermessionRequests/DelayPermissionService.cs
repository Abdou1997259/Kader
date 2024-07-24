using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Interfaces;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.Services.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class DelayPermissionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IDelayPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;


        #region ListOfIncreaseSalaryRequest

        public async Task<Response<IEnumerable<DTODelayPermissionRequest>>> ListOfDelayPermissionRequest()
        {
            var result = await unitOfWork.DelayPermission.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = sharLocalizer[Localization.NotFound];
            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<IEnumerable<DTODelayPermissionRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion


         

        #region insert

        public async Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<DelayPermission>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AtachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await _unitOfWork.DelayPermission.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };


        }
        #endregion



        #region Delete
        public async Task<Response<string>> DeleteDelayPermissionRequest(int id, string fullPath)
        {
            var obj = await unitOfWork.DelayPermission.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            unitOfWork.DelayPermission.Remove(obj);
            if (await unitOfWork.CompleteAsync() > 0)
                _fileServer.RemoveFile(fullPath, obj.AtachmentPath);

            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }
        #endregion

        #region Read
        public async Task<Response<GetAllDelayRequestRespond>> GetAllDelayPermissionRequsts(GetAlFilterationDelayPermissionReuquest model, string host)
        {
          
            Expression<Func<DelayPermission, bool>> filter = x => x.IsDeleted == false;

            var totalRecords = await _unitOfWork.DelayPermission.CountAsync(filter: filter);
            var items = await _unitOfWork.DelayPermission.GetSpecificSelectAsync(filter, x => x, orderBy: x => x.OrderBy(x => x.Id),
                skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, includeProperties: "Employee"
            );
            var mappeditems = _mapper.Map<List<DtoListOfDelayRequestReponse>>(items);
            #region Pagination

            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            #endregion

            var result = new GetAllDelayRequestRespond
            {
                TotalRecords = totalRecords,
                Items =mappeditems,
                CurrentPage = model.PageNumber,
                FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
                From = (page - 1) * model.PageSize + 1,
                To = Math.Min(page * model.PageSize, totalRecords),
                LastPage = totalPages,
                LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
                PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
                NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
                Path = host,
                PerPage = model.PageSize,
                Links = pageLinks,
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new()
                    {
                        Items = []
                    },
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };
        }
        #endregion

        #region Update

        public async Task<Response<DtoListOfDelayRequestReponse>> UpdateDelayPermissionRequest(int id,DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
       

            var leave = await _unitOfWork.DelayPermission.GetByIdAsync(id);
            if (leave == null)
            {
                var msg = _sharLocalizer[Localization.NotFound];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedleave = _mapper.Map(model, leave);
            _unitOfWork.DelayPermission.Update(mappedleave);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);

            leave.AtachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);


            var full_path = Path.Combine(root, clientName, moduleName);
            if (model.Attachment != null)
                _fileServer.RemoveFile(full_path, leave.AtachmentPath);


            _unitOfWork.DelayPermission.Update(leave);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }

        //    var newRequest = _mapper.Map<DelayPermission>(model);
        //    var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);

        //    newRequest.AtachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
        //        await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);

        //    var Oldrequest = await _unitOfWork.DelayPermission.GetByIdWithNoTrackingAsync(model.EmployeeId);
        //    var full_path = Path.Combine(root, clientName, moduleName);
        //    if (Oldrequest.AtachmentPath != null)
        //        _fileServer.RemoveFile(full_path, Oldrequest.AtachmentPath);


        //    _unitOfWork.DelayPermission.Update(newRequest);
        //    var result = await _unitOfWork.CompleteAsync();
        //    return new()
        //    {
        //        Msg = sharLocalizer[Localization.Done],
        //        Check = true,
        //    };
        //}



        #endregion

        #region GetById
        public  async Task<Response<DtoListOfDelayRequestReponse>> GetById(int id)
        {
            var result = await unitOfWork.DelayPermission.GetByIdAsync(id);
            if (result == null)
            {
                var msg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }

            var mappingResult = mapper.Map<DtoListOfDelayRequestReponse>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };
        }


        #endregion


    }
}
