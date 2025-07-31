namespace EventTicketingSystem.CSharp.Domain.Features.Dashboard;

public class DA_Dashboard
{
    private readonly AppDbContext _db;
    private readonly ILogger<DA_Dashboard> _logger;

    public DA_Dashboard(AppDbContext db, ILogger<DA_Dashboard> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Result<DashboardResponseModel>> GetDashboardSummary()
    {
        var response = new Result<DashboardResponseModel>();
        try
        {
            var totalEvent = await _db.TblEvents.CountAsync(x => !x.Deleteflag);
            var totalVenue = await _db.TblVenues.CountAsync(x => !x.Deleteflag);
            var totalAdmin = await _db.TblAdmins.CountAsync(x => !x.Deleteflag);
            var totalBO = await _db.TblBusinessowners.CountAsync(x => !x.Deleteflag);

            // Top 3 Ticket Types by ticket count
            var ticketCounts = await (
                from tt in _db.TblTickettypes
                where !tt.Deleteflag
                join tp in _db.TblTicketprices.Where(x => !x.Deleteflag) on tt.Tickettypecode equals tp.Tickettypecode
                join t in _db.TblTickets.Where(x => !x.Deleteflag) on tp.Ticketpricecode equals t.Ticketpricecode
                group t by new { tt.Tickettypename } into g
                orderby g.Count() descending
                select new DashboardTicketCount
                {
                    Label = g.Key.Tickettypename,
                    TotalCount = g.Count()
                }
            ).Take(3).ToListAsync();

            // Ticket Sales by month (successful transactions only, Jan-Dec current year)
            var currentYear = DateTime.UtcNow.Year;
            var salesByMonth = await (
                from tr in _db.TblTransactions
                where !tr.Deleteflag
                      && tr.Status == "success"
                      && tr.Transactiondate.Year == currentYear
                group tr by tr.Transactiondate.Month into g
                select new
                {
                    Month = g.Key,
                    TotalCount = g.Count()
                }
            ).ToListAsync();

            // Prepare sales for all months Jan-Dec
            var ticketSales = Enumerable.Range(1, 12)
                .Select(m => new DashboardTicketSale
                {
                    Month = new DateTime(currentYear, m, 1).ToString("MMM"),
                    TotalCount = salesByMonth.FirstOrDefault(x => x.Month == m)?.TotalCount ?? 0
                })
                .ToList();

            var data = new DashboardResponseModel
            {
                TotalEvent = totalEvent,
                TotalVenue = totalVenue,
                TotalAdmin = totalAdmin,
                TotalBO = totalBO,
                TicketCounts = ticketCounts,
                TicketSales = ticketSales
            };

            response = Result<DashboardResponseModel>.Success("Here is the dashboard data!", data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dashboard summary");
            response = Result<DashboardResponseModel>.SystemError();
        }
        return response;
    }
}