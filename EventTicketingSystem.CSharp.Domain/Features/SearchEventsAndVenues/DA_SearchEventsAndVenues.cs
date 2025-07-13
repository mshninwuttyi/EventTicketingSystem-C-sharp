using EventTicketingSystem.CSharp.Domain.Models.Features.SearchEventsAndVenues;

namespace EventTicketingSystem.CSharp.Domain.Features.SearchEventsAndVenues;

public class DA_SearchEventsAndVenues
{
    private readonly ILogger<DA_SearchEventsAndVenues> _logger;

    private readonly AppDbContext _db;
    public DA_SearchEventsAndVenues(ILogger<DA_SearchEventsAndVenues> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Result<SearchListEventsAndVenuesResponseModel>> SearchEventsAndVenues(string searchTerm)
    {
        try
        {
            var EventResult = await _db.TblEvents
                .Where(e => EF.Functions.ILike(e.Eventname!, "%" + searchTerm + "%"))
            .Select(e => new SearchEventResponseModel
            {
                Eventname = e.Eventname
            })
            .ToListAsync();

            var VenueResult = await _db.TblVenues
                .Where(v => EF.Functions.ILike(v.Venuename!, "%" + searchTerm + "%"))
                .Select(v => new SearchVenuesResponseModel
                {
                    Venuename = v.Venuename
                })
                .ToListAsync();

            var SearchResult = new SearchListEventsAndVenuesResponseModel
            {
                Events = EventResult,
                Venues = VenueResult
            };

            return Result<SearchListEventsAndVenuesResponseModel>.Success(SearchResult);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<SearchListEventsAndVenuesResponseModel>.SystemError(ex.Message);
        }
    }
}
