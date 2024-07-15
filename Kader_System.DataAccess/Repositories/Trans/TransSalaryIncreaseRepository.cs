using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Trans;
using Kader_System.Domain.DTOs.Response.Loan;
using Kader_System.Domain.DTOs.Response.Trans;
using Kader_System.Domain.Interfaces;
using Kader_System.Domain.Models.Trans;
using Kader_System.Domain.DTOs.Request.HR;
using Kader_System.Domain;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
    {
       
    }
}
