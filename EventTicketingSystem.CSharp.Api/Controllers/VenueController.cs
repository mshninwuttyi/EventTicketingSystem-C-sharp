using EventTicketingSystem.CSharp.Domain.Features.Venue;
using EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

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
    public async Task<IActionResult> Get()
    {
        var result = await _blService.GetList();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VenueRequestModel request)
    {
        // Get login user ID from claims
        var currentUserId = "AD000001";  // To Edit: Get Login User ID from the incoming request later
        
        var result = await _blService.CreateVenue(request, currentUserId);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return StatusCode(500, new { message = result.Message });
    }

    [HttpPut("{venueId}")]
    public async Task<IActionResult> Update(string venueId, [FromBody] VenueRequestModel request)
    {
        // validate the Venue ID in URL matches the Venue ID in request body
        if (venueId != request.VenueId)
        {
            return BadRequest(new { message = "Venue ID mismatch." });
        }
        
        // Get login user ID from claims
        var currentUserId = "AD000001";   // To Edit: Get Login User ID from the incoming request later

        var result = await _blService.UpdateVenue(request, currentUserId);

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
        // Get login user ID from claims
        var currentUserId = "AD000001";  // To Edit: Get Login User ID from the incoming request later
        
        var result = await _blService.DeleteVenue(venueId, currentUserId);
        
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