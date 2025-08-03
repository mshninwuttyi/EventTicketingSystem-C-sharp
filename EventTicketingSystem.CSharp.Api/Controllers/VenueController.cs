namespace EventTicketingSystem.CSharp.Api.Controllers;

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

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        var data = await _blVenue.List();
        return Ok(data);
    }

    [HttpGet("Edit/{venueCode}")]
    public async Task<IActionResult> Edit(string venueCode)
    {
        var data = await _blVenue.Edit(venueCode);
        return Ok(data);
    }

    [HttpPost("Create")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(VenueCreateRequestModel requestModel)
    {
        var data = await _blVenue.Create(requestModel);
        return Ok(data);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(VenueUpdateRequestModel requestModel)
    {
        var data = await _blVenue.Update(requestModel);
        return Ok(data);
    }

    [HttpPost("Delete/{venueCode}")]
    public async Task<IActionResult> Delete(string venueCode)
    {
        var data = await _blVenue.Delete(venueCode);
        return Ok(data);
    }
}