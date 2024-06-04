namespace Kader_System.Services.IServices.HR;

public interface IContractService
{
    Task<Response<IEnumerable<ListOfContractsResponse>>> ListOfContractsAsync(string lang);
    Task<Response<GetAllContractsResponse>> GetAllContractAsync(string lang, GetAlFilterationForContractRequest model,string host);

    Task<Response<GetAllContractsResponse>> GetAllEndContractsAsync(string lang,
        GetAlFilterationForContractRequest model, string host);
    Task<Response<CreateContractRequest>> CreateContractAsync(CreateContractRequest model);
    Task<Response<GetContractByIdResponse>> GetContractByIdAsync(int id,string lang);
    Task<Response<object>> GetLookUps(string lang);
    Task<Response<CreateContractRequest>> UpdateContractAsync(int id, CreateContractRequest model);
    Task<Response<CreateContractRequest>> RestoreContractAsync(int id);
    Task<Response<string>> UpdateActiveOrNotContractAsync(int id);
    Task<Response<string>> DeleteContractAsync(int id);
}
