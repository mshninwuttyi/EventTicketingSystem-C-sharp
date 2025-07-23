namespace EventTicketingSystem.CSharp.Domain.Features.BusinessOwner;

public class BL_BusinessOwner
{
    private readonly DA_BusinessOwner _daService;

    public BL_BusinessOwner(DA_BusinessOwner dataAccess)
    {
        _daService = dataAccess;
    }

    public async Task<Result<BusinessOwnerResponseModel>> GetList()
    {
        return await _daService.GetList();
    }

    public async Task<Result<BusinessOwnerResponseModel>> GetBusinessOwner(BusinessOwnerRequestModel request)
    {
        return await _daService.GetByCode(request.Businessownercode);
    }

    public async Task<Result<BusinessOwnerResponseModel>> CreateNewBusinessOwner(BusinessOwnerRequestModel request)
    {
        return await _daService.CreateBusinessOwner(request);
    }

    public async Task<Result<BusinessOwnerResponseModel>> UpdateBusinessOwner(BusinessOwnerRequestModel request)
    {
        return await _daService.UpdateBusinessOwner(request);
    }

    public async Task<Result<BusinessOwnerResponseModel>> DeleteBusinessOwner(BusinessOwnerRequestModel request)
    {
        return await _daService.DeleteOwnerByCode(request.Businessownercode);
    }
}