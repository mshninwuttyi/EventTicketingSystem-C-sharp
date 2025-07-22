namespace EventTicketingSystem.CSharp.Domain.Features.BusinessEmail;

public class BL_BusinessEmail
{
    private readonly DA_BusinessEmail _da_BusinessEmail;

    public BL_BusinessEmail(DA_BusinessEmail da_BusinessEmail)
    {
        _da_BusinessEmail = da_BusinessEmail;
    }

    public async Task<Result<BusinessEmailResponseModel>> Create(BusinessEmailRequestModel requestModel)
    {
        return await _da_BusinessEmail.Create(requestModel);
    }

    public async Task<Result<BusinessEmailResponseModel>> GetById(string id)
    {
        return await _da_BusinessEmail.GetById(id);
    }

    public async Task<Result<BusinessEmailListResponseModel>> GetList()
    {
        return await _da_BusinessEmail.GetList();
    }
}