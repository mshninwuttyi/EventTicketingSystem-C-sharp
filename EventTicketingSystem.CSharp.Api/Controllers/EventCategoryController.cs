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

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        return Ok(await _blEventCategory.List());
    }

    [HttpGet("Edit/{eventCategoryCode}")]
    public async Task<IActionResult> Edit(string eventCategoryCode)
    {
        return Ok(await _blEventCategory.Edit(eventCategoryCode));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] EventCategoryCreateRequestModel requestModel)
    {
        return Ok(await _blEventCategory.Create(requestModel));
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] EventCategoryUpdateRequestModel requestModel)
    {
        return Ok(await _blEventCategory.Update(requestModel));
    }

    [HttpPost("Delete/{eventCategoryCode}")]
    public async Task<IActionResult> Delete(string eventCategoryCode)
    {
        return Ok(await _blEventCategory.Delete(eventCategoryCode));
    }
}