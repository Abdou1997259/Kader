//using Kader_System.Domain.Constant;
//using Kader_System.Domain.Models.HR;

//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Localization;

//namespace Kader_System.Services.Services.HR
//{
//    public class StructureMangement : IStructureMangement
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IStringLocalizer<SharedResource> _sharLocalizer;

//        public StructureMangement(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer)
//        {
//            _unitOfWork = unitOfWork;
//            _sharLocalizer = sharLocalizer;
//        }

//        public async Task<Response<object>> GetStructureMangementAsync(int companyId,string lang)
//        {
//            // Step 1: Fetch the company and check if it exists
//            var company = await _unitOfWork.Companies.GetByIdAsync(companyId);
//            if (company == null)
//            {
//                var msg = $"{_sharLocalizer[Localization.Company]} {_sharLocalizer[Localization.NotFound]}";
//                return new Response<object> { Check = false, Msg = msg, Data = null };
//            }

//            // Step 2: Fetch all managements and related departments in one query
//            var managements = await _unitOfWork.Managements
//                .GetSpecificSelectAsync(
//                    m => m.CompanyId == companyId,
//                    m => new { Management = m, Departments = m.HrDepartments.ToList() }
//                );

//            // Step 3: Build the tree structure
//            var tree = new TreeNode<object>(company);

//            foreach (var item in managements)
//            {
//                var management = item.Management;
//                var departments = item.Departments;

//                var managementNode = tree.Add(management);

//                // Add departments to the management node
//                foreach (var department in departments)
//                {
//                    var departmentNode = managementNode.Add(department);

//                    // Step 4: Fetch employees for each department and add them to the tree
//                    var employees = await _unitOfWork.Employees
//                        .GetSpecificSelectAsync(
//                            e => e.DepartmentId == department.Id,
//                            e => e
//                        );

//                    foreach (var employee in employees)
//                    {
//                        departmentNode.Add(employee);
//                    }
//                }

//                //var companyreponse = tree.Value as HrCompany;
//                // var  = tree.Childern.Values() as IEnumerable<ManagementResponse>;
//                //var mangementresponse
//                var result = new CompanyResponse
//                {
//                    Name = Localization.Arabic == lang ? companyreponse.NameAr : companyreponse.NameEn,
//                    Id = companyreponse.Id,
//                    Managements = tree.Childern.Values() as IEnumerable<ManagementResponse>

//                };
//            }

//            // Step 5: Convert tree structure to the desired result format
          

//            return new Response<object> { Check = true, Data = result, Msg = null };
//        }

//    }
//}
