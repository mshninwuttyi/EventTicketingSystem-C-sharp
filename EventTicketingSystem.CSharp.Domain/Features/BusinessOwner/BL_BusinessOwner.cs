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
}