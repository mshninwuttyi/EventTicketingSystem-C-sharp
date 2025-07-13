namespace EventTicketingSystem.CSharp.Domain.Models.Features.SearchEventsAndVenues;

public class SearchListEventsAndVenuesResponseModel
{
    public List<SearchEventResponseModel> Events { get; set; } = new List<SearchEventResponseModel>();
    public List<SearchVenuesResponseModel> Venues { get; set; } = new List<SearchVenuesResponseModel>();
}

public class SearchEventResponseModel
{
    public string? Eventname { get; set; }
}

public class SearchVenuesResponseModel
{
    public string? Venuename { get; set; }
}
