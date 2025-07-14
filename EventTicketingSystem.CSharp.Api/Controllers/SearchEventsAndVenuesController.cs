using EventTicketingSystem.CSharp.Domain.Features.SearchEventsAndVenues;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SearchEventsAndVenuesController : Controller
{
    private readonly BL_SearchEventsAndVenues _bl_SearchEventsAndVenues;

    public SearchEventsAndVenuesController(BL_SearchEventsAndVenues bl_SearchEventsAndVenues)
    {
        _bl_SearchEventsAndVenues = bl_SearchEventsAndVenues;
    }

    [HttpGet]
    public IActionResult SearchEventsAndVenues([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return BadRequest("Search term cannot be null or empty.");
        }

        var result = _bl_SearchEventsAndVenues.SearchEventsAndVenues(searchTerm).Result;

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }
}
