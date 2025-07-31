namespace EventTicketingSystem.CSharp.Domain.Models.Features.Dashboard;

public class DashboardResponseModel
{
    public int TotalEvent { get; set; }

    public int TotalVenue { get; set; }

    public int TotalAdmin { get; set; }

    public int TotalBO { get; set; }

    public List<DashboardTicketCount> TicketCounts { get; set; }

    public List<DashboardTicketSale> TicketSales {get; set;}
}

public class DashboardTicketCount
{
    public string Label { get; set; }
    public int TotalCount { get; set; }
}

public class DashboardTicketSale
{
    public string Month { get; set; }
    public int TotalCount { get; set; }
}