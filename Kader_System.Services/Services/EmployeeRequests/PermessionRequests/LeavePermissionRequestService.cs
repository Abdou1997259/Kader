
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService : ILeavePermissionRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model)
        public Task<List<DTOLeavePermissionRequest>> GetAllLeavePermissionRequests()
        {

        }
        public Task<int> AddNewLeavePermissionRequest(DTOLeavePermissionRequest model) { }
        public Task<int> UpdateLeavePermissionRequest(DTOLeavePermissionRequest model) { }
        public Task<int> DeleteLeavePermissionRequest(int id) { }
    }
}
