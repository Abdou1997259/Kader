
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ILeavePermissionRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string root, string clientName, string moduleName,HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
            var newRequest = _mapper.Map<LeavePermissionRequest>(model);            
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await _unitOfWork.LeavePermissionRequest.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
        #endregion

        #region Read
        public async Task<Response<GetAllLeavePermissionRequestResponse>> GetAllLeavePermissionRequsts(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host)
        {
            var list = await _unitOfWork.LeavePermissionRequest.GetWithJoinAsync(
                x=>x.IsDeleted == model.IsDeleted &&
                x.StatuesOfRequest.ApporvalStatus==null,"Employee");
            var query = from q in list
                       
                        select new
                        {
                            q.Id,
                            RequestDate = q.Add_date,
                            EmployeeName = Localization.Arabic == lang ? q.Employee.FirstNameAr : q.Employee.FirstNameEn,
                            q.LeaveTime,
                            q.BackTime,
                            q.StatuesOfRequest.ApporvalStatus,
                            Attachment = q.AttachmentPath,

                        };
            #region Pagination
            var totalRecords = query.Count();
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

            var result = new GetAllLeavePermissionRequestResponse
            {
                TotalRecords = totalRecords,
                Items = query.OrderByDescending(x => x.Id).Cast<object>().ToList(),
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
        public async Task<Response<DTOLeavePermissionRequest>> UpdateLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
            var newRequest = _mapper.Map<LeavePermissionRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            _unitOfWork.LeavePermissionRequest.Update(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
    }
}
