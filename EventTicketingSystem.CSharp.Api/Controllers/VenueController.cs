namespace EventTicketingSystem.CSharp.Api.Controllers;

[Authorize]
[Tags("Venue")]
[Route("api/[controller]")]
[ApiController]
public class VenueController : ControllerBase
{   
    private readonly BL_Venue _blVenue;
    public VenueController(BL_Venue blService)
    {
        _blVenue = blService;
    }
    
    // Get current AdminCode from JWT claims
    private string CurrentUserId => User.GetCurrentUserId();

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var data = await _blVenue.List();
        return Ok(data);
    }

    [HttpGet("Edit/{venueId}")]
    public async Task<IActionResult> Edit(string venueId)
    {
        var data = await _blVenue.Edit(venueId);
        return Ok(data);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] VenueCreateRequestModel requestModel)
    {
        var data = await _blVenue.Create(requestModel);
        return Ok(data);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] VenueUpdateRequestModel requestModel)
    {
        var data = await _blVenue.Update(requestModel);
        return Ok(data);
    }

    [HttpPost("Delete/{venueId}")]
    public async Task<IActionResult> Delete(string venueId)
    {
        var data = await _blVenue.Delete(venueId);
        return Ok(data);
    }
}