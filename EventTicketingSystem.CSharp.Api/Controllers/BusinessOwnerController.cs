namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Business Owner")]
[Route("api/[controller]")]
[ApiController]
public class BusinessOwnerController : ControllerBase
{
    private readonly BL_BusinessOwner _blService;

    public BusinessOwnerController(BL_BusinessOwner blService)
    {
        _blService = blService;
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _blService.GetList());
    }
}