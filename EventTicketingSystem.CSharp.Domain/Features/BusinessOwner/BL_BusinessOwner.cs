namespace EventTicketingSystem.CSharp.Domain.Features.BusinessOwner;

public class BL_BusinessOwner
{
    private readonly DA_BusinessOwner _dataAccess;

    public BL_BusinessOwner(DA_BusinessOwner dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<Result<BusinessOwnerResponseModel>> GetList(BusinessOwnerRequestModel requestModel)
    {
        return await _dataAccess.GetList(requestModel);
    }
}