
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.HR;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR
{
    public class FingerPrintDeviceService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper) : IFingerPrintDeviceService
    {
        private HrFingerPrint _fingerPrintInstance;
      
        #region MyRegion

        public async Task<Response<GetAllFingerPrintDevicesResponse>> GetAllFingerPrintDevicesAsync(string lang,
            GetAllFingerPrintDevicesFilterrationRequest model,string host)
        {
            Expression<Func<HrFingerPrint, bool>> filter = x => x.IsDeleted == model.IsDeleted
                && (string.IsNullOrEmpty(model.Word)
                ||x.NameAr.Contains(model.Word)
                ||x.NameEn.Contains(model.Word)
                ||x.IPAddress.Contains(model.Word)
                ||x.Port.Contains(model.Word));

            var totalRecords = await unitOfWork.FingerPrints.CountAsync(filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllFingerPrintDevicesResponse
            {
                TotalRecords = totalRecords,
                Items = (await unitOfWork.FingerPrints.GetSpecificSelectAsync(filter: filter,
                    includeProperties: $"{nameof(_fingerPrintInstance.Company)}",
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select:
                    d => new FingerPrintDeviceData()
                    {
                        Id = d.Id,
                        NameAr = d.NameAr,
                        NameEn = d.NameEn,
                        IPAddress = d.IPAddress,
                        Port = d.Port,
                        CompanyId = d.CompanyId,
                        CompanyName = lang == Localization.Arabic ? d.Company!.NameAr : d.Company!.NameEn,
                        Username = d.Username,
                        Password = d.Password

                    })).ToList(),
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
                Links = pageLinks

            };
            if (result.TotalRecords is 0)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetFingerPrintDeviceByIdResponse>> GetFingerPrintDeviceByIdAsync(int id)
        {
            Expression<Func<HrFingerPrint, bool>> filter = x => x.Id == id;
            var obj = await unitOfWork.FingerPrints.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_fingerPrintInstance.Company)}");

            if (obj is null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.NotFoundData], shareLocalizer[Localization.FingerPrintDevice]);

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };

            }

            return new()
            {
                Check = true,
                Error = string.Empty,
                Msg = string.Empty,
                IsActive = obj.IsActive,
                Data = mapper.Map<GetFingerPrintDeviceByIdResponse>(obj),
            };
        }

        public async Task<Response<IEnumerable<ListOfFingerPrintDevicesResponse>>> GetFingerPrintDevicesAsync(string lang)
        {
            Expression<Func<HrFingerPrint, bool>> filter = x => x.IsDeleted == false;
            var result=  unitOfWork.FingerPrints.GetSpecificSelectAsync(filter,
                select: (d => new ListOfFingerPrintDevicesResponse()
                {
                    Id = d.CompanyId,
                    Name = lang==Localization.Arabic ? d.NameAr:d.NameEn,
                    IPAddress = d.IPAddress,
                    Port = d.Port,
                    CompanyId = d.CompanyId
                })).Result.ToList();
            if (!result.Any())
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = [],
                    Error = resultMsg,
                    Msg = resultMsg,
                    Check = false
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };
        }


        public async Task<Response<FingerPrintLookupDataResponse>> GetFingerPrintsLookUpsData(string lang)
        {
            try
            {
                var companies = await unitOfWork.Companies.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,

                    });


                return new Response<FingerPrintLookupDataResponse>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new FingerPrintLookupDataResponse()
                    {
                        companies = companies.ToArray(),
                       
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<FingerPrintLookupDataResponse>()
                {
                    Error = exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }
        #endregion


        #region Create
        public async Task<Response<CreateFingerPrintDeviceRequest>> CreateFingerPrintDevicesAsync(CreateFingerPrintDeviceRequest request)
        {
            var exists = await unitOfWork.FingerPrints.ExistAsync(x => x.NameAr.Trim() == request.NameAr.Trim()
                                                                    && x.NameEn.Trim() == request.NameEn.Trim());

            if (exists)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                    shareLocalizer[Localization.FingerPrintDevice]);
                return new()
                {
                    Check = false,
                    Data = request,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var newDevice = mapper.Map<HrFingerPrint>(request);
            await unitOfWork.FingerPrints.AddAsync(newDevice);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = string.Format(shareLocalizer[Localization.Done],
                    shareLocalizer[Localization.FingerPrintDevice]),
                Check = true,
                Data = request
            };
        }


        #endregion

        #region Update
        public async Task<Response<CreateFingerPrintDeviceRequest>> UpdateFingerPrintDevicesAsync(int id, CreateFingerPrintDeviceRequest request)
        {
            var obj = await unitOfWork.FingerPrints.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.NotFoundData],
                    shareLocalizer[Localization.FingerPrintDevice]);
                return new()
                {
                    Check = false,
                    Data = request,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.Username = request.Username;
            obj.Password = request.Password;
            obj.CompanyId=request.CompanyId;
            obj.IPAddress=request.IPAddress;
            obj.Port=request.Port;
            obj.NameAr=request.NameAr;
            obj.NameEn=request.NameEn;
            unitOfWork.FingerPrints.Update(obj);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Data = request,
                Error = string.Empty,
                Msg = shareLocalizer[Localization.Done],
                IsActive = obj.IsActive
            };
        }
        public Task<Response<string>> UpdateActiveOrNotEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<object>> RestoreFingerPrintAsync(int id)
        {
            var obj = await unitOfWork.FingerPrints.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.FingerPrintDevice]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.IsDeleted = false;
            unitOfWork.FingerPrints.Update(obj);
            await unitOfWork.CompleteAsync();
            var newObject = new
            {
                Id = obj.Id,
                NameAr = obj.NameAr,
                NameEn = obj.NameEn
            };
            return new()
            {
                Data = newObject
                ,
                Error = string.Empty,
                Msg = shareLocalizer[Localization.Restored],
                Check = true
            };
        }
        #endregion


        #region Delete

        public async Task<Response<string>> DeleteFingerPrintAsync(int id)
        {
            var obj = await unitOfWork.FingerPrints.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.NotFoundData],
                    shareLocalizer[Localization.FingerPrintDevice]);
                return new()
                {
                    Check = false,
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            try
            {
                unitOfWork.FingerPrints.Remove(obj);
                await unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Data = string.Empty,
                    Error = string.Empty,
                    Msg = shareLocalizer[Localization.Deleted]
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Check = false,
                    Data = string.Empty,
                    Error = e.Message,
                    Msg = e.Message
                };
            }
        }

        #endregion

    }
}
