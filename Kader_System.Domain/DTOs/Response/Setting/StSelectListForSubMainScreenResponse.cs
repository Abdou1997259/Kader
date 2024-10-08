﻿namespace Kader_System.Domain.DTOs.Response.Setting;

public class StSelectListForSubMainScreenResponse : PaginationRequest
{
    public int Id { get; set; }
    public string Screen_sub_title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int? Screen_main_cat_id { get; set; }

    public List<StScreenCat> List_Screen_main_cat { get; set; }
}
