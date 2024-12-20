﻿using Kader_System.DataAccess.DesginPatterns;
using Kader_System.Domain.DTOs.Request.Setting;
using Microsoft.Data.SqlClient;

namespace Kader_System.DataAccess.Repositories
{
    public class StoredProcuduresRepo(KaderDbContext db) : IStoredProcuduresRepo
    {
        private readonly KaderDbContext _db = db;

        public async Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly startCalculationDate,
            int days, string listEmployeesString, int? companyId, int? departmentId, int? empId)
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
                .FromSqlInterpolated($@"exec sp_calculate_salary {startOfMonth}, {endCalculationDate}, {empsParameter} ,{companyId},{departmentId},{empId}")
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SpCacluateSalary>> Sp_GET_EMP_SALARY(DateOnly startCalculationDate,
        int days, int? companyId, int? departmentId, int? empId)
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


            // Execute stored procedure and return result
            var result = await _db.SpCacluateSalariesModel
                .FromSqlInterpolated($@"exec Sp_GET_EMP_SALARY {startOfMonth}, {endCalculationDate} ,{companyId},{departmentId},{empId}")
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<SpCaclauateSalaryDetails>>
       CalculateSalaryDetails(DateOnly startCalculationDate,
       DateOnly EndCalculationDate, string listEmployeesString)
        {
            // Adjust endCalculationDate to the last day of the month with the specified day

            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec sp_calculate_salary_details {startCalculationDate}, {EndCalculationDate}, {empsParameter}").ToListAsync();
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
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec sp_calculate_salary_details {startOfMonth}, {endCalculationDate}, {empsParameter}").ToListAsync();
            return result;

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
            var result = await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec sp_calculate_salary_details {startCalculationDate}, {endCalculationDate}, {empsParameter}").ToListAsync();





            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedInfo(DateOnly startCalculationDate, DateOnly endCalculationDate, string listEmployeesString)
        {

            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            IEnumerable<SpCaclauateSalaryDetailedTrans> result = null;


            result = await _db.SpCaclauateSalaryDetailedTransModel.FromSqlInterpolated($"exec sp_calculate_salary_detailed_info {startCalculationDate}, {endCalculationDate}, {empsParameter}").ToListAsync();



            return result;

        }
        public async Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedInfo(DateOnly startCalculationDate, int days, string listEmployeesString)
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



            var result = await _db.SpCaclauateSalaryDetailedTransModel.FromSqlInterpolated($"exec sp_calculate_salary_detailed_info {startOfMonth}, {endCalculationDate}, {empsParameter}").ToListAsync();



            return result;

        }
        public async Task<GetMyProfilePermissionAndScreen> SpGetScreen(string userId, int titleId, string lang)
        {

            var userlogincontext = await UserPermissionFactory.CreatePermissionsUserStrategy(_db, userId, titleId, lang);
            var rawData = await userlogincontext.GetPermissions();

            rawData = rawData.Distinct().ToList();
            var data = rawData
                    .GroupBy(s => s.main_id)
                    .Select(x => new GetAllStMainScreen
                    {
                        Id = x.Key,
                        main_title = x.FirstOrDefault()?.main_title,
                        main_image = x.FirstOrDefault()?.main_image,
                        cats = x.GroupBy(c => c.cat_id).Select(cg => new GetAllStMainScreenCat
                        {
                            Id = cg.FirstOrDefault().cat_id,
                            main_id = cg.Key,
                            title = cg.FirstOrDefault()?.cat_title,
                            main_image = x.FirstOrDefault()?.main_image,
                            subs = cg.Select(s => new GetAllStScreenSub
                            {
                                sub_title = s.sub_title,
                                main_id = s.main_id,
                                sub_image = Path.Combine(Modules.Setting, s.main_image == null ? "" : s.main_image),
                                screen_code = s.screen_code,
                                Sub_Id = s.sub_id,
                                Screen_CatId = s.cat_id,
                                actions = s.actions,
                                cat_Title = s.cat_title,
                                permissions = s.permission,
                                main_title = s.main_title,
                                url = s.url,

                            }).ToList()
                        }).ToList()
                    }).ToList();



            var permission = rawData

            .Select(x => new Dictionary<string, GetUserPermission>
            {
                {
                    x.screen_code,
                    new GetUserPermission
                    { actions = x.actions,
                    TitleId = titleId,
                    cat_id = x.cat_id,
                    cat_title = x.cat_title,
                    main_id = x.main_id,
                    main_img =Path.Combine(Modules.Setting, x.main_image ==null? "":x.main_image),
                    main_title = x.main_title,
                    permissions = x.permission.Split(',')
                                       .Select(p => int.TryParse(p, out var result) ? result : 0) // Handle conversion with fallback value
                                       .ToList(),
                    screen_code = x.screen_code,
                    sub_id = x.sub_id,
                    sub_title = x.sub_title,
                    url = x.url

                    }
                }


            }).ToList();






            return new GetMyProfilePermissionAndScreen
            {
                getAllStMainScreens = data,
                myPermissions = permission
            };
        }

        public async Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, DateOnly EndCalculationDate, string listEmployeesString, string EmpId,
            int? companyId, int? departmentId, int? empId)
        {

            var empsParameter = new SqlParameter
            {
                ParameterName = "@listEmployeesString",
                SqlDbType = SqlDbType.VarChar,
                Value = listEmployeesString
            };
            return await _db.SpCaclauateSalaryDetailsModel.FromSqlInterpolated($"exec sp_calculate_salary_details {startCalculationDate}, {EndCalculationDate}, {empsParameter} ,{companyId},{departmentId},{empId}").ToListAsync();
        }

        public async Task<IEnumerable<Get_Details_Calculations>> Get_Details_Calculations(
            DateOnly startCalculationDate, DateOnly EndCalculationDate, int? companyId,
            int? departmentId, int? empId, int? mangementId, int lang)
        {



            // Adjust endCalculationDate to the last day of the month with the specified day

            return await _db.Get_Details_Calculations.FromSqlInterpolated(@$"exec Get_Details_Calculations
              {startCalculationDate}, {EndCalculationDate} ,{companyId},{departmentId},{empId},{mangementId},{lang}").ToListAsync();





        }
    }
}
