

namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class GetSalaryCalculatorFilterRequest : PaginationRequest
    {
        public Status? status { get; set; } = Status.None;

    }
}
