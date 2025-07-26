namespace EventTicketingSystem.CSharp.Domain.Features.Venue;

public class BL_Venue
{
    private readonly DA_Venue _daService;

    public BL_Venue(DA_Venue dataAccess)
    {
        _daService = dataAccess;
    }

    public async Task<Result<VenueListResponseModel>> List()
    {
        return await _daService.List();
    }

    public async Task<Result<VenueEditResponseModel>> Edit(string venueId)
    {
        return await _daService.Edit(venueId);
    }

    public async Task<Result<VenueCreateResponseModel>> Create(VenueCreateRequestModel requestModel)
    {
        return await _daService.Create(requestModel);
    }

    public async Task<Result<VenueUpdateResponseModel>> Update(VenueUpdateRequestModel requestModel)
    {
        return await _daService.Update(requestModel);
    }

    public async Task<Result<VenueDeleteResponseModel>> Delete(string venueId)
    {
        return await _daService.Delete(venueId);
    }
}