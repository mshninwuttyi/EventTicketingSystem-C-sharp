﻿namespace EventTicketingSystem.CSharp.Domain.Models.Features.Dashboard;

public class DashboardResponseModel
{
    public DashboardCount TotalEvent { get; set; }

    public DashboardCount TotalVenue { get; set; }

    public DashboardCount TotalAdmin { get; set; }

    public DashboardCount TotalBO { get; set; }

    public List<DashboardTTCount> TicketCounts { get; set; }

    public List<DashboardTicketSale> TicketSales {get; set;}
}

public class DashboardCount
{
    public int TotalCount { get; set; }
    public int Difference { get; set; }
}

public class DashboardTTCount
{
    public TTCountType Type { get; set; }
    public List<TTCount> TTCounts { get; set; }
}

public class TTCount
{
    public string Label { get; set; }
    public int TotalCount { get; set; }
}

public class DashboardTicketSale
{
    public string Month { get; set; }
    public int TotalCount { get; set; }
}

public enum TTCountType
{
    Week,
    Month
}