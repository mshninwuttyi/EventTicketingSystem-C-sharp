namespace EventTicketingSystem.CSharp.Domain.Models.Features.SearchEventsAndVenues;

public class SearchListEventsResponseModel
{
    public List<SearchEventResponseModel> Events { get; set; } = new List<SearchEventResponseModel>();
}