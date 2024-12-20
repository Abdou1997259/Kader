﻿namespace Kader_System.Domain.DTOs.Response.Interview
{
    public class GetAllApplicantsResponse : PaginationData<ApplicantList>
    {
    }
    public class ApplicantList
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public float? rate { get; set; }
        public int gender { get; set; }
        public int state { get; set; }
        public string image_path { get; set; }
        public int age { get; set; }
    }
}
