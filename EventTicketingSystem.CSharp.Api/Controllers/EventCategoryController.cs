using System.Diagnostics;

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
        if (requestModel.CategoryName == null)
        {
            return BadRequest("Category Name cannot be null.");
        }   
        var response = await _blEventCategory.CreateEventCategory(requestModel);

        if (response.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }
        return Ok(response);
    }

    [HttpPost("UpdateEventCategory/{eventCategoryCode}")]
    public async Task<IActionResult> UpdateEventCategory(string eventCategoryCode,[FromBody] EventCategoryRequestModel request)
    {    
        request.EventCategoryCode = eventCategoryCode;
        var response = await _blEventCategory.UpdateEventCategory(request);
        if (response.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }
        return Ok(response);
    }

    [HttpPost("DeleteEventCategory/{eventCategoryCode}")]
    public async Task<IActionResult> DeleteEventCategory(string eventCategoryCode)
    {
        var request = new EventCategoryRequestModel();
        request.EventCategoryCode = eventCategoryCode;
        var response = await _blEventCategory.DeleteEventCategory(request);
        if (response.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
        }
        return Ok(response.Message);
    }

    #endregion
}