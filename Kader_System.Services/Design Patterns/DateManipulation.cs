namespace Kader_System.Services.Design_Patterns
{
    public static class DateManipulation
    {
        public static (DateOnly startDate, DateOnly endDate) GetLastDateOfMonth(
            DateOnly startCalculationDate, int startday)
        {
            int year = startCalculationDate.Year;
            int month = startCalculationDate.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            startday = Math.Min(daysInMonth, startday);
            var startOfMonth = new DateOnly(year, month, startday);


            // Adjust endCalculationDate to the last day of the month with the specified day
            var endCalculationDate = new DateOnly(year, month, startday).AddMonths(1).AddDays(-1);
            return (startOfMonth, endCalculationDate);
        }
    }
}
