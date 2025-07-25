using EventTicketingSystem.CSharp.Shared;

namespace EventTicketingSystem.CSharp.Api.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _blService.List();
        return Ok(result);
    }

    [HttpGet("{venueId}")]
    public async Task<IActionResult> Edit(string venueId)
    {
        var result = await _blService.Edit(venueId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VenueCreateRequestModel request)
    {
        // Get login user ID from claims
        //var currentUserId = "AD000001";  // To Edit: Get Login User ID from the incoming request later
        
        var result = await _blService.Create(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        
        return StatusCode(500, new { message = result.Message });
    }

    [HttpPut("{venueId}")]
    public async Task<IActionResult> Update(string venueId, [FromBody] VenueUpdateRequestModel request)
    {
        _logger.LogInformation("Incoming UpdateVenue Request: {Request}", request.ToJson());
        
        // validate the Venue ID in URL matches the Venue ID in request body
        if (venueId != request.VenueId)
        {
            return BadRequest(new { message = "Venue ID mismatch." });
        }
        
        // Get login user ID from claims
        //var currentUserId = "AD000001";   // To Edit: Get Login User ID from the incoming request later

        var result = await _blService.Update(request);

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
        //var currentUserId = "AD000001";  // To Edit: Get Login User ID from the incoming request later
        
        var result = await _blService.Delete(venueId);
        
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