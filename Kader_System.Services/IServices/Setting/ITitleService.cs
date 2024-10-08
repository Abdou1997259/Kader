﻿

using Kader_System.Domain.DTOs.Request.Auth;

namespace Kader_System.Services.IServices.Setting
{
    public interface ITitleService
    {
        Task<Response<IEnumerable<SelectListOfTitleResponse>>> ListOfTitlesAsync(string lang);
        Task<Response<GetAllTitleResponse>> GetAllTitlesAsync(string lang, GetAllFilterrationForTitleRequest model, string host);
        Task<Response<CreateTitleRequest>> CreateTitleAsync(CreateTitleRequest model);

        Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id, string lang);

        Task<Response<UpdateTitleRequest>> UpdateTitleAsync(int id, UpdateTitleRequest model,string lang,bool all);
        public  Task<Response<string>> RestoreTitleAsync(int id);

        Task<Response<string>> UpdateActiveOrNotTitleAsync(int id);
        Task<Response<string>> DeleteTitleAsync(int id);

    }
}
