using Microsoft.AspNetCore.Authorization;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Authorize]
[Tags("Venue")]
[Route("api/[controller]")]
[ApiController]
public class VenueController : ControllerBase
{   
    private readonly BL_Venue _blService;
    private readonly ILogger<VenueController> _logger;

    public VenueController(BL_Venue blService, ILogger<VenueController> logger)
    {
        _blService = blService;
        _logger = logger;
    }
    
    // Get current AdminCode from JWT claims
    private string CurrentUserId => User.GetCurrentUserId(_logger);

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _blService.GetList();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVenueRequestModel request)
    {
        var result = await _blService.CreateVenue(request, CurrentUserId);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return StatusCode(500, new { message = result.Message });
    }

    [HttpPut("{venueId}")]
    public async Task<IActionResult> Update(string venueId, [FromBody] UpdateVenueRequestModel request)
    {
        // validate the Venue ID in URL matches the Venue ID in request body
        if (venueId != request.VenueId)
        {
            return BadRequest(new { message = "Venue ID mismatch." });
        }

        var result = await _blService.UpdateVenue(request, CurrentUserId);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        if (result.Message == "Venue not found.")
        {
            return NotFound(new { message = result.Message});
        }
        
        return StatusCode(500, new { message = result.Message });
    }
    
    [HttpDelete("{venueId}")]
    public async Task<IActionResult> Delete(string venueId)
    {
        var result = await _blService.DeleteVenue(venueId, CurrentUserId);
        
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        
        if (result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound(new { message = result.Message });
        }
        
        return StatusCode(500, new { message = result.Message });
    }
}