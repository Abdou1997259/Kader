﻿



using Kader_System.Domain.DTOs.Response.Trans;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kader_System.DataAccess.Repositories.Setting
{
    public class TitleRepository(KaderDbContext context) : BaseRepository<Title>(context), ITitleRepository
    {
        private readonly KaderDbContext _context;



        //public async Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id,string lang)
        //{

        //var obj = await context.Titles.Where(t => t.Id == id)
        //    .Include(t => t.TitlePermissions)
        //    .Include(t => t.TitlePermissions.Select(p => p.ScreenSub))
        //    .ThenInclude(t => t.ScreenCat)
        //    //.ThenInclude(t => t.MainScreen)
        //    .Select(t => new GetTitleByIdResponse()
        //    {
        //        Id = t.Id,
        //        TitleNameAr = t.TitleNameAr,
        //        TitleNameEn = t.TitleNameEn,
        //        all_permissions = t.TitlePermissions.Select(p => new GetTitlePermissionResponse()
        //        {
        //            sub_title = p.ScreenSub!.Screen_sub_title_ar,

        //            cat_id = p.ScreenSub!.ScreenCatId,
        //            cat_title = p.ScreenSub!.ScreenCat!.Screen_cat_title_ar,
        //            main_id = p.ScreenSub!.ScreenCatId,
        //            main_title = p.ScreenSub!.ScreenCat!.screenCat!.Screen_main_title_ar,
        //            main_image = p.ScreenSub!.ScreenCat!.screenCat!.Screen_main_image,
        //            sub_id = p.SubScreenId,
        //            actions = "",

        //        }).ToList()
        //    }).FirstOrDefaultAsync();

        //if (obj is null)
        //{
        //    string resultMsg = "Not Found";

        //    return new()
        //    {
        //        Data = new(),
        //        Error = resultMsg,
        //        Msg = resultMsg
        //    };
        //}

        //return new()
        //{
        //    Data = obj,
        //    Check = true
        //};
        //}
        public Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id, string lang)
        {
            throw new NotImplementedException();
        }


    }

}
