using AutoMapper;
using Kader_System.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices
{
    public interface ITransSalaryIncreaseService
    {
        public  Task<Response<IEnumerable<SelectListOfTransSalaryIncrementResponse>>> ListOfTransSalaryIncreaseAsync(string lang);
        public  Task<Response<GetAllSalaryIncreaseResponse>> GetAllTransSalaryIncreaseAsync(string lang, GetAlFilterationForSalaryIncreaseRequest model, string host);


        public Task<Response<CreateTransSalaryIncreaseRequest>> CreateTransSalaryIncreaseAsync(CreateTransSalaryIncreaseRequest model,string lang);

        public Task<Response<TransSalaryIncreaseResponse>> GetTransSalaryIncreaseByIdAsync(int id, string lang);

        public Task<Response<CreateTransSalaryIncreaseRequest>> UpdateTransSalaryIncreaseAsync(CreateTransSalaryIncreaseRequest model);
        public Task<Response<object>> RestoreTransSalaryIncreaseAsync(int id);
        public Task<Response<string>> DeleteTransSalaryIncreaseAsync(int id);

    }
}
