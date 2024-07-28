using Kader_System.Domain.Constant;
using Kader_System.Domain.Models.HR;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Kader_System.Services.Services.HR
{
    public class StructureMangement : IStructureMangement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer;

        public StructureMangement(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharLocalizer = sharLocalizer;
        }

        public async Task<Response<CompanyResponse>> GetStructureMangementAsync(int companyId, string lang)
        {
            // Step 1: Fetch the company and check if it exists
            var company = await _unitOfWork.Companies.GetByIdAsync(companyId);
            var companies=await _unitOfWork.Companies.GetAllAsync();    
            var emps=await _unitOfWork.Employees.GetSpecificSelectAsync(x=>true,x=>x); 
            var mangs=await _unitOfWork.Managements.GetSpecificSelectAsync(x=>true,x=>x,includeProperties: "Manager");
            var departs = await _unitOfWork.Departments.GetSpecificSelectAsync(x=>true,x=>x,includeProperties: "Manager");
            if (company == null)
            {
                var msg = $"{_sharLocalizer[Localization.Company]} {_sharLocalizer[Localization.NotFound]}";
                return new () { Check = false, Msg = msg, Data = null };
            }

            // Step 2: Fetch all managements and related departments in one query
            var managements = await _unitOfWork.Managements
                .GetSpecificSelectAsync(
                    m => m.CompanyId == companyId,
                    m => new { Management = m, Departments = m.HrDepartments.ToList() }
                );

            // Step 3: Build the tree structure
            var tree = new TreeNode<object>(company);

            foreach (var item in managements)
            {

                var management = mangs.FirstOrDefault(x=>x.Id==item.Management.Id);
                var departments = item.Departments;

                var managementNode = tree.Add(management);

                // Add departments to the management node
                foreach (var department in departments)
                {
                    var depart= departs.FirstOrDefault(x=>x.Id== department.Id);
                    var departmentNode = managementNode.Add(depart);

                    // Step 4: Fetch employees for each department and add them to the tree
                    var employees = await _unitOfWork.Employees
                        .GetSpecificSelectAsync(
                            e => e.DepartmentId == department.Id,
                            e => e,
                            includeProperties: "Job"

                        );

                    foreach (var employee in employees)
                    {
                        departmentNode.Add(employee);
                    }
                }

                //var companyreponse = tree.Value as HrCompany;
                // var  = tree.Childern.Values() as IEnumerable<ManagementResponse>;
                //var mangementresponse
              
            }

            // Step 5: Convert tree structure to the desired result format

            var result = tree.ToCompanyResponse(lang);
            return new (){ Check = true, Data = result, Msg = null, LookUps = new {
                Companies=companies.Select(x=>new CompanyLookup
                {
                    Id = x.Id,
                    CompnayName =Localization.Arabic==lang?x.NameAr:x.NameEn
                    
                }),
                Employees=emps.Select(x=>new Empolyeelookups
                {
                    Id = x.Id,
                    Name =Localization.Arabic==lang?x.FullNameAr:x.FullNameEn,
                    DepartmentId=x.DepartmentId,
                    MangmentId=x.ManagementId,
                   
                }),
                Mangements= mangs.Select(x=>new ManagementLookup
                {
                    Id=x.Id,
                    ManagementName=Localization.Arabic==lang?x.NameAr:x.NameEn,
                    ManagerId=x.ManagerId,
                    CompanyId=x.CompanyId,
                }),
                Department= departs.Select(x=>new DepartmentLookup
                {
                    Id=x.Id,
                    DepartmentName= Localization.Arabic == lang ? x.NameAr : x.NameEn,
                    ManagementId=x.ManagementId,
                    ManagerId=x.ManagerId,
                   
                }),
            
            
            } };
        }

    }
}
