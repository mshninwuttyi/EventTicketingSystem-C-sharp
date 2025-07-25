namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Venue")]
[Route("api/[controller]")]
[ApiController]
public class VenueController : ControllerBase
{
    private readonly BL_Venue _blService;

    public VenueController(BL_Venue blService)
    {
        _blService = blService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _blService.List());
    }

    [HttpGet("Edit/{venueId}")]
    public async Task<IActionResult> Edit(string venueId)
    {
        return Ok(await _blService.Edit(venueId));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] VenueCreateRequestModel requestModel)
    {
        return Ok(await _blService.Create(requestModel));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] VenueUpdateRequestModel requestModel)
    {
        return Ok(await _blService.Update(requestModel));
    }

    [HttpDelete("Delete/{venueId}")]
    public async Task<IActionResult> Delete(string venueId)
    {
        return Ok(await _blService.Delete(venueId));
    }
}