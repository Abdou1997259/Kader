using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Request.Setting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        public async Task<GetMyProfilePermissionAndScreen> SpGetScreen(string userId,int titleId,string lang)
        {
            List<SpGetScreen> rawData = null;
            if (userId != "b74ddd14-6340-4840-95c2-db12554843e5basb1")
            {
                        rawData = await _db.Set<SpGetScreen>()
                .FromSqlRaw("EXEC sp_get_screen @UserId, @TitleId, @Lang",
                            new SqlParameter("@UserId", userId),
                            new SqlParameter("@TitleId", titleId),
                            new SqlParameter("@Lang", lang)).AsNoTracking().ToListAsync();
            }
            else
            {

                rawData = await _db.Set<SpGetScreen>()
                    .FromSqlRaw("EXEC sp_get_Super_Admin_screen @Lang",
                    new SqlParameter("@Lang", lang)).AsNoTracking().ToListAsync();

            }
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
            //    g => g.Key, // The key for the dictionary is the screen_code
            //    g => g.Select(x => new GetUserPermission
            //    {
            //        actions = x.actions,
            //        TitleId = titleId,
            //        cat_id = x.cat_id,
            //        cat_title = x.cat_title,
            //        main_id = x.main_id,
            //        main_img =Path.Combine(Modules.Setting, x.main_image ==null? "":x.main_image),
            //        main_title = x.main_title,
            //        permissions = x.permission.Split(',')
            //                           .Select(p => int.TryParse(p, out var result) ? result : 0) // Handle conversion with fallback value
            //                           .ToList(), 
            //        screen_code = x.screen_code,
            //        sub_id = x.sub_id,
            //        sub_title = x.sub_title,
            //        url = x.url
            //    }).FirstOrDefault() // Convert the IEnumerable to a List
            //).ToList();




        

            return new GetMyProfilePermissionAndScreen
            {
                getAllStMainScreens = data,
                myPermissions = permission
            };
        }
    }
}
