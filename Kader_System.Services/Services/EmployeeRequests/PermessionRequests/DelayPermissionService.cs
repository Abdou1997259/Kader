using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class DelayPermissionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IDelayPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model)
        {
            var newTrans = _mapper.Map<DelayPermission>(model);
            await _unitOfWork.DelayPermission.AddAsync(newTrans);
            await _unitOfWork.CompleteAsync();
            

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };




        }
    }
}
