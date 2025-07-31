namespace EventTicketingSystem.CSharp.Domain.Features.Dashboard;

public class BL_Dashboard
{
    private readonly DA_Dashboard _daService;

    public BL_Dashboard(DA_Dashboard daService)
    {
        _daService = daService;
    }

    public async Task<Result<DashboardResponseModel>> GetDashboardData()
    {
        return await _daService.GetDashboardSummary();
    }
}
