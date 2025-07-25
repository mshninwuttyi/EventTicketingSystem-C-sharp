namespace EventTicketingSystem.CSharp.Domain.Models.Features.SearchEventsAndVenues;

public class SearchListEventsByAmountResponseModel
{
    public List<SearchEventResponseModel> Events { get; set; } = new List<SearchEventResponseModel>();

    public List<SearchTicketPriceResponseModel> TicketPrice { get; set; } = new List<SearchTicketPriceResponseModel>();
}

public class SearchTicketPriceResponseModel
{
    public string Ticketpriceid { get; set; } = null!;

    public string Ticketpricecode { get; set; } = null!;

    public string Eventcode { get; set; } = null!;

    public string Tickettypecode { get; set; } = null!;

    public decimal Ticketprice { get; set; }

    public int Ticketquantity { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }
}