using Microsoft.Data.SqlClient;

namespace Kader_System.DataAccess.Repositories
{
    public class StoredProcuduresRepo(KaderDbContext db) : IStoredProcuduresRepo
    {
        private readonly KaderDbContext _db = db;
        public async Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly startCalculationDate, int days, string listEmployeesString)
        {
            // Calculate the end of the month based on startCalculationDate
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            days = Math.Min(daysInMonth, days);
            var startOfMonth = new DateOnly(year, month, days);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);



            // Adjust endCalculationDate to the last day of the month with the specified day
            var endCalculationDate = new DateOnly(year, month, days).AddMonths(1).AddDays(-1);

            // Create SqlParameter for listEmployeesString
            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };

            // Execute stored procedure and return result
            var result = await _db.SpCacluateSalariesModel
                .FromSqlInterpolated($"exec Sp_Cacluate_Salary {startOfMonth}, {endCalculationDate}, {empsParameter}")
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, int days, string listEmployeesString)
        {
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            days = Math.Min(daysInMonth, days);
            var startOfMonth = new DateOnly(year, month, days);


            // Adjust endCalculationDate to the last day of the month with the specified day
            var endCalculationDate = new DateOnly(year, month, days).AddMonths(1).AddDays(-1);
            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec Sp_Cacluate_Salary_Details {startOfMonth}, {endCalculationDate}, {empsParameter}").ToListAsync();
            return null;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, DateOnly endCalculationDate, string listEmployeesString)
        {



            // Adjust endCalculationDate to the last day of the month with the specified day

            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec Sp_Cacluate_Salary_Details {startCalculationDate}, {endCalculationDate}, {empsParameter}").ToListAsync();





            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedTrans(DateOnly startCalculationDate, DateOnly endCalculationDate, string listEmployeesString)
        {

            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            IEnumerable<SpCaclauateSalaryDetailedTrans> result = null;


            result = await _db.SpCaclauateSalaryDetailedTransModel.FromSqlInterpolated($"exec Sp_Cacluate_Salary_DetailedTrans {startCalculationDate}, {endCalculationDate}, {empsParameter}").ToListAsync();



            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedTrans(DateOnly startCalculationDate, int days, string listEmployeesString)
        {

            int daysInMonth = DateTime.DaysInMonth(startCalculationDate.Year, startCalculationDate.Month);
            days = Math.Min(daysInMonth, days);
            var startOfMonth = new DateOnly(startCalculationDate.Year, startCalculationDate.Month, days);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            // Adjust endCalculationDate to the last day of the month with the specified day
            var endCalculationDate = new DateOnly(startCalculationDate.Year, startCalculationDate.Month, days).AddMonths(1).AddDays(-1);



            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };



            var result = await _db.SpCaclauateSalaryDetailedTransModel.FromSqlInterpolated($"exec Sp_Cacluate_Salary_DetailedTrans {startOfMonth}, {endCalculationDate}, {empsParameter}").ToListAsync();



            return result;

        }
    }
}
