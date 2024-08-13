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
    public class GetAllScreensService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer) : IGetAllScreensService
    {
        //public async Task<Response<Dictionary<string, List<Dictionary<string, object>>>>> GetAllScreens(string lang)
        //{
        //    var langParameter = new SqlParameter("@Lang", lang);

        //    var results = await _context.SpGetAllScreens
        //        .FromSqlRaw("EXEC sp_GetAllScreens @Lang", langParameter)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    // Initialize the output structure
        //    var output = new Dictionary<string, List<Dictionary<string, object>>>
        //    {
        //        { "main", new List<Dictionary<string, object>>() },
        //        { "cat", new List<Dictionary<string, object>>() },
        //        { "sub", new List<Dictionary<string, object>>() }
        //    };

        //    // Process the results
        //    foreach (var item in results)
        //    {
        //        var mainItem = new Dictionary<string, object>
        //        {
        //            { "id", item.main_id },
        //            { "title", item.main_title },
        //            { "image", item.Screen_main_cat_image }
        //        };

        //        var catItem = new Dictionary<string, object>
        //        {
        //            { "id", item.cat_id },
        //            { "main_id", item.mainScreen.Id },
        //            { "title", item.cat_title },
        //            { "Main_Title", item.mainScreen.Screen_main_title_ar },
        //        };

        //        var subItem = new Dictionary<string, object>
        //        {
        //            { "id", item.sub_id },
        //            { "cat_id", item.catscreen.Id },
        //            { "cat_id", item.catscreen.Screen_cat_title_ar },
        //            { "title", item.sub_title },
        //            { "url", item.url },
        //            { "screen_code", item.screen_code }
        //        };

        //        // Add to output dictionaries
        //        output["main"].Add(mainItem);
        //        output["cat"].Add(catItem);
        //        output["sub"].Add(subItem);
        //    }

        //    return new Response<Dictionary<string, List<Dictionary<string, object>>>>
        //    {
        //        Check = true,
        //        DynamicData = output,
        //        Error = ""
        //    };

        //    #region Recent
        //    //public async Task<Response<Dictionary<string, List<Dictionary<string, object>>>>> GetAllScreens(string lang)
        //    //{
        //    //    var langParameter = new SqlParameter("@Lang", lang);

        //    //    var results = await _context.SpGetAllScreens
        //    //        .FromSqlRaw("EXEC sp_GetAllScreens @Lang", langParameter)
        //    //        .AsNoTracking()
        //    //        .ToListAsync();



        //    //    var output = new Dictionary<string, List<Dictionary<string, object>>>();

        //    //    // Group the results by category type
        //    //    var groupedResults = results.GroupBy(x => x.main_title);

        //    //    foreach (var group in groupedResults)
        //    //    {
        //    //        var categoryType = group.Key;
        //    //        var categoryList = new List<Dictionary<string, object>>();

        //    //        foreach (var item in group)
        //    //        {
        //    //            var categoryItem = new Dictionary<string, object>
        //    //            {
        //    //                { "title", item.cat_title },
        //    //                { "id", item.ca }
        //    //                // Add more properties as needed
        //    //            };
        //    //            categoryList.Add(categoryItem);
        //    //        }

        //    //        output.Add(categoryType, categoryList);
        //    //    }

        //    //    return new Response<Dictionary<string, List<Dictionary<string, object>>>>
        //    //    {
        //    //        Check = true,
        //    //        DynamicData = output,
        //    //        Error = ""
        //    //    };





        //    //}
        //    #endregion

        //}
        //}
        public async Task<Response<ScreenLookups>> GetAllScreensAsync(string lang)
        {
            var mains =await  unitOfWork.MainScreens.GetAllAsync();
            List<ScreenMainLookup> lookupsForMain = mains.Where(x => !x.IsDeleted).OrderBy(x=>x.Order).Select(x => new ScreenMainLookup
            {
                Id = x.Id,
                main_image = Path.Combine(SD.GoRootPath.GetSettingImagesPath, x.Screen_main_image ?? " "),
                main_title = Localization.Arabic == lang ? x.Screen_main_title_ar : x.Screen_main_title_en
            }).ToList();

            List<ScreenCatLookup> lookupsForCat = (await unitOfWork.MainScreenCategories.GetSpecificSelectAsync(x => !x.IsDeleted, select: x => new ScreenCatLookup
            {
                Id = x.Id,
                cat_title = Localization.Arabic == lang ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                main_id = x.screenCat.Id,
                Main_Title = Localization.Arabic == lang ? x.screenCat.Screen_main_title_ar : x.screenCat.Screen_main_title_en

            }, includeProperties: "screenCat",orderBy:x=>x.OrderBy(s=>s.Order))).ToList();


            List<ScreenSubLookup> lookupsForsub = (await unitOfWork.SubMainScreens.GetSpecificSelectAsync(x => !x.IsDeleted, select: x => new ScreenSubLookup
            {
                sub_id = x.Id,
                cat_id = x.ScreenCat.Id,
                Cat_title = Localization.Arabic == lang ? x.ScreenCat.Screen_cat_title_ar : x.ScreenCat.Screen_cat_title_en,
                sub_title = Localization.Arabic == lang ? x.Screen_sub_title_ar : x.Screen_sub_title_en,
                screen_code=x.ScreenCode,
                url=x.Url

            }, includeProperties: "ScreenCat", orderBy: x => x.OrderBy(s => s.Order))).ToList();

            return new()
            {
                Data = new ScreenLookups
                {
                    cat = lookupsForCat,
                    main = lookupsForMain,
                    sub = lookupsForsub
                },
                Check = true,


            };
        }
    }
}
