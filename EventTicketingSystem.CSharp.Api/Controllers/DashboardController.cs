namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Dashboard")]
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly BL_Dashboard _blDashboard;

    public DashboardController(BL_Dashboard bL_Dashboard)
    {
        _blDashboard = bL_Dashboard;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetDashboardData()
    {
        return Ok(await _blDashboard.GetDashboardData());
    }
}
