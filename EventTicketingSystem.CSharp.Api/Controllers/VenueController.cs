using EventTicketingSystem.CSharp.Domain.Features.Venue;

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
    
    [HttpDelete("{venueId}")]
    public async Task<IActionResult> Delete(string venueId)
    {
        var result = await _blService.DeleteVenue(venueId);
        
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