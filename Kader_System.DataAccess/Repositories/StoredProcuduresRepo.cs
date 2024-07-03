using Microsoft.Data.SqlClient;

namespace Kader_System.DataAccess.Repositories
{
    public class StoredProcuduresRepo(KaderDbContext db) : IStoredProcuduresRepo
    {
        private readonly KaderDbContext _db = db;
        public async Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly startCalculationDate, int days, string listEmployeesString)
        {
            // Assuming model.StartCalculationDate is already DateOnly
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            startCalculationDate = new DateOnly(year, month, days);
            var endCalculationDate = startCalculationDate.AddMonths(1).AddDays(-1);

            var empsParamter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString,


            };
            var result = await _db.SpCacluateSalariesModel.FromSql($"exec Sp_Cacluate_Salary {startCalculationDate},{endCalculationDate},{empsParamter}").ToListAsync();



            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, int days, string listEmployeesString)
        {
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            startCalculationDate = new DateOnly(year, month, days);
            var endCalculationDate = startCalculationDate.AddMonths(1).AddDays(-1);
            var empsParamter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString,


            };
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSql($"exec Sp_Cacluate_Salary_Details {startCalculationDate},{endCalculationDate},{empsParamter}").ToListAsync();





            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedTrans(DateOnly startCalculationDate, int days, string listEmployeesString)
        {
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            startCalculationDate = new DateOnly(year, month, days);
            var endCalculationDate = startCalculationDate.AddMonths(1).AddDays(-1);
            var empsParamter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString,


            };

            var result = await _db.SpCaclauateSalaryDetailedTransModel.FromSql($"exec Sp_Cacluate_Salary_DetailedTrans {startCalculationDate},{endCalculationDate},{empsParamter}").ToListAsync();



            return result;

        }
    }
}
