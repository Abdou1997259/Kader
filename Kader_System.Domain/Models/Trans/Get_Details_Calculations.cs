namespace Kader_System.Domain.Models.Trans
{
    public class Get_Details_Calculations
    {
        public int? Id { get; set; } // Employee ID
        public string? FullName { get; set; } // Full name in Arabic
        public string? PaymentWay { get; set; }
        public double? WorkingDays { get; set; } // Number of working days
        public double? Allowance { get; set; }
        // Financial Details
        public double? FixedSalary { get; set; } // Base salary
        public double? HousingAllowance { get; set; } // Housing allowance
        public double? Benefit { get; set; }// Additional benefits
        public double? AdditionalValues { get; set; } // Any other additions
        public double? Deduction { get; set; } // Deductions
        public double? Loan { get; set; } // Loan amount
        public double? MinuesValues { get; set; } // Other subtractions
        public double? AbsSum { get; set; } // Absence deductions
        public double? AbsDays { get; set; } // Absence days
        public double? Total { get; set; } // Total calculated salary
    }

}
