namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class GetLookupsCalculatedSalaries
    {
        public List<Empolyeelookups> EmployeeLookups { get; set; }
        public List<CompanyLookup> CompanyLookups { get; set; }
        public List<ManagementLookup> ManagementLookups { get; set; }
        public List<DepartmentLookup> DepartmentLookups { get; set; }
    }
    public class CompanyLookup
    {
        public string CompnayName { get; set; }
        public int Id { get; set; }

    }
    public class DepartmentLookup
    {
        public string DepartmentName { get; set; }
        public int Id { get; set; }
    }
    public class ManagementLookup
    {
        public string ManagementName { get; set; }
        public int Id { get; set; }
    }
    public class Empolyeelookups
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
