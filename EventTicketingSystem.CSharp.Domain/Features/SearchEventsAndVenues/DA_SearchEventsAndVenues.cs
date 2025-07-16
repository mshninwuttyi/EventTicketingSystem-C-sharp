using EventTicketingSystem.CSharp.Domain.Models.Features.SearchEventsAndVenues;
using EventTicketingSystem.CSharp.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

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
                .Where(e => EF.Functions.ILike(e.Eventname!, "%" + searchTerm + "%")
                && e.Deleteflag == false)
            .Select(e => new SearchEventResponseModel
            {
                Eventid = e.Eventid,
                Eventcode = e.Eventcode,
                Eventname = e.Eventname,
                Categorycode = e.Categorycode,
                Description = e.Description,
                Address = e.Address,
                Startdate = e.Startdate,
                Enddate = e.Enddate,
                Eventimage = e.Eventimage,
                Isactive = e.Isactive,
                Eventstatus = e.Eventcode,
                Businessownercode = e.Businessownercode,
                Totalticketquantity = e.Totalticketquantity,
                Soldoutcount = e.Soldoutcount,
                Createdby = e.Createdby,
                Createdat = e.Createdat,
                Modifiedby = e.Modifiedby,
                Modifiedat = e.Modifiedat
            })
            .AsNoTracking()
            .ToListAsync();

            var VenueResult = await _db.TblVenues
                .Where(v => EF.Functions.ILike(v.Venuename!, "%" + searchTerm + "%")
                && v.Deleteflag == false)
                .Select(v => new SearchVenuesResponseModel
                {
                    Venueid = v.Venueid,
                    Venuecode = v.Venuecode,
                    Venuename = v.Venuename,
                    Venuedetailcode = v.Venuedetailcode,
                    Venuetypecode = v.Venuetypecode,
                    Venuedescription = v.Venuedescription,
                    Venueaddress = v.Venueaddress,
                    Venuecapacity = v.Venuecapacity,
                    Venuefacilities = v.Venuefacilities,
                    Venueaddons = v.Venueaddons,
                    Createdby = v.Createdby,
                    Createdat = v.Createdat,
                    Modifiedby = v.Modifiedby,
                    Modifiedat = v.Modifiedat
                })
                .AsNoTracking()
                .ToListAsync();

            var SearchResult = new SearchListEventsAndVenuesResponseModel
            {
                Events = EventResult,
                Venues = VenueResult
            };

            var eventsData = SearchResult.Events.Count();
            var venuesData = SearchResult.Venues.Count();
            if (eventsData == 0 && venuesData == 0)
            {
                return Result<SearchListEventsAndVenuesResponseModel>.NotFoundError("No events or venues found matching the search term.");
            }

            return Result<SearchListEventsAndVenuesResponseModel>.Success(SearchResult);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<SearchListEventsAndVenuesResponseModel>.SystemError(ex.Message);
        }
    }
}
