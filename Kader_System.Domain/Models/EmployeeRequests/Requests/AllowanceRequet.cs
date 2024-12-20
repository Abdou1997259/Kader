﻿namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_allowance_request")]
    public class AllowanceRequest : BaseEntity
    {
        public int Id { get; set; }
        public double amount { get; set; }
        public string? notes { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime allowance_request_date { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }
        public int allowance_id { get; set; }
        [ForeignKey(nameof(allowance_id))]
        public virtual HrAllowance HrAllowance { get; set; }
        public int allowance_type_id { get; set; }
        [ForeignKey(nameof(allowance_type_id))]
        public int company_id { get; set; }
        


        public StatuesOfRequest? StatuesOfRequest { get; set; }

    }
}
