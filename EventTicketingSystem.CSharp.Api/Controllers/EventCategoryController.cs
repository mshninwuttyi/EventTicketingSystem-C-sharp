namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Event Category")]
[Route("api/[controller]")]
[ApiController]
public class EventCategoryController : ControllerBase
{
    private readonly BL_EventCategory _blEventCategory;

    public EventCategoryController(BL_EventCategory blEventCategory)
    {
        _blEventCategory = blEventCategory;
    }

    #region Event Category

    [HttpGet("GetEventCategory")]
    public async Task<IActionResult> GetCategoryList()
    {
        return Ok(await _blEventCategory.GetList());
    }

    [HttpPost("CreateEventCategory")]
    public async Task<IActionResult> CreateEventCategory([FromBody] EventCategoryRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest("Request model cannot be null.");
        }
        var response = await _blEventCategory.CreateEventCategory(requestModel);

        if (response.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }
        return Ok(response.Data);
    }

    [HttpPost("UpdateEventCategory")]
    public async Task<IActionResult> UpdateEventCategory([FromBody] EventCategoryRequestModel request)
    {
        return Ok(await _blEventCategory.UpdateEventCategory(request));
    }

    [HttpDelete("DeleteEventCategory/{eventCategoryCode}")]
    public async Task<IActionResult> DeleteEventCategory(string eventCategoryCode)
    {
        var request = new EventCategoryRequestModel { EventCategoryCode = eventCategoryCode };
        return Ok(await _blEventCategory.DeleteEventCategory(request));
    }

    #endregion
}