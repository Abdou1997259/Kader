﻿namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetSalaryCalculatorList
    {
        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }
        public DateOnly CalculationDate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string JobName { get; set; }
        public string AddedBy { get; set; }




    }
}