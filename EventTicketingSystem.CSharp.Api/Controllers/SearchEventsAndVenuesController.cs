using EventTicketingSystem.CSharp.Shared;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Search Menu")]
[ApiController]
[Route("api/[controller]")]
public class SearchEventsAndVenuesController : ControllerBase
{
    private readonly BL_SearchEventsAndVenues _bl_SearchEventsAndVenues;

    public SearchEventsAndVenuesController(BL_SearchEventsAndVenues bl_SearchEventsAndVenues)
    {
        _bl_SearchEventsAndVenues = bl_SearchEventsAndVenues;
    }

    [HttpGet]
    public IActionResult SearchEventsAndVenues([FromBody] string searchTerm)
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

        return Ok(result);
    }

    [HttpGet]
    public IActionResult SearchEventsByDate([FromBody] DateTime StartDate, DateTime EndDate)
    {
        if (StartDate.IsNullOrEmpty())
        {
            return BadRequest("Search date cannot be null or empty.");
        }

        var result = _bl_SearchEventsAndVenues.SearchEventsByDate(StartDate, EndDate).Result;

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }
        return Ok(result);
    }
}