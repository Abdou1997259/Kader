using Kader_System.DataAccesss.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.Setting
{
    public class GetAllScreensService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer) : IGetAllScreensService
    {
        public async Task<Response<Dictionary<string, List<Dictionary<string, object>>>>> GetAllScreens(string lang)
        {
            var langParameter = new SqlParameter("@Lang", lang);

            var results = await _context.SpGetAllScreens
                .FromSqlRaw("EXEC sp_GetAllScreens @Lang", langParameter)
                .AsNoTracking()
                .ToListAsync();

            var output = new Dictionary<string, List<Dictionary<string, object>>>
            {
                { "main", new List<Dictionary<string, object>>() },
                { "cat", new List<Dictionary<string, object>>() },
                { "sub", new List<Dictionary<string, object>>() }
            };

          
            foreach (var item in results)
            {
                var mainItem = new Dictionary<string, object>
                {
                    { "id", item.main_id },
                    { "main_title", item.main_title },
                    { "main_image", item.Screen_main_cat_image }
                };

                var catItem = new Dictionary<string, object>
                {
                    { "id", item.cat_id },
                    { "main_id", item.main_id },
                    { "cat_title", item.cat_title },
                    { "Main_Title", item.main_title},
                };

                var subItem = new Dictionary<string, object>
                {
                    { "sub_id", item.sub_id },
                    { "cat_id", item.cat_id },
                    { "Cat_title", item.cat_title },
                    { "sub_title", item.sub_title },
                    { "url", item.url },
                    { "screen_code", item.screen_code }
                };

             
                output["main"].Add(mainItem);
                output["cat"].Add(catItem);
                output["sub"].Add(subItem);
            }

            return new Response<Dictionary<string, List<Dictionary<string, object>>>>
            {
                Check = true,
                DynamicData = output,
                Error = ""
            };

            #region Recent
            //public async Task<Response<Dictionary<string, List<Dictionary<string, object>>>>> GetAllScreens(string lang)
            //{
            //    var langParameter = new SqlParameter("@Lang", lang);

            //    var results = await _context.SpGetAllScreens
            //        .FromSqlRaw("EXEC sp_GetAllScreens @Lang", langParameter)
            //        .AsNoTracking()
            //        .ToListAsync();



            //    var output = new Dictionary<string, List<Dictionary<string, object>>>();

            //    // Group the results by category type
            //    var groupedResults = results.GroupBy(x => x.main_title);

            //    foreach (var group in groupedResults)
            //    {
            //        var categoryType = group.Key;
            //        var categoryList = new List<Dictionary<string, object>>();

            //        foreach (var item in group)
            //        {
            //            var categoryItem = new Dictionary<string, object>
            //            {
            //                { "title", item.cat_title },
            //                { "id", item.ca }
            //                // Add more properties as needed
            //            };
            //            categoryList.Add(categoryItem);
            //        }

            //        output.Add(categoryType, categoryList);
            //    }

            //    return new Response<Dictionary<string, List<Dictionary<string, object>>>>
            //    {
            //        Check = true,
            //        DynamicData = output,
            //        Error = ""
            //    };





            //}
            #endregion

        }
    }   
}
