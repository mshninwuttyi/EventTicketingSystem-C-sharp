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

    public async Task<Result<VenueResponseModel>> CreateVenue(VenueRequestModel venue, string currentUserId)
    {
        return await _daService.CreateVenue(venue, currentUserId);
    }

    public async Task<Result<VenueResponseModel>> UpdateVenue(VenueRequestModel venue, string currentUserId)
    {
        return await  _daService.UpdateVenue(venue, currentUserId);
    }
    
    public async Task<Result<VenueResponseModel>> DeleteVenue(string venueId, string currentUserId)
    {
        return await _daService.DeleteVenue(venueId, currentUserId);
    }
}