using EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

namespace EventTicketingSystem.CSharp.Domain.Features.Venue;

public class BL_Venue
{
    private readonly DA_Venue _daService;

    public BL_Venue(DA_Venue dataAccess)
    {
        _daService = dataAccess;
    }

    public async Task<Result<List<VenueResponseModel>>> GetList()
    {
        return await _daService.GetList();
    }
    
    public async Task<Result<VenueResponseModel>> DeleteVenue(string venueId)
    {
        return await _daService.DeleteVenue(venueId);
    }
}