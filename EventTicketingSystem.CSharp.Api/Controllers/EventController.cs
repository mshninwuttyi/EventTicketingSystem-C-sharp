namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Event")]
[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly BL_Event _blEvent;

    public EventController(BL_Event blEvent)
    {
        _blEvent = blEvent;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        var data = await _blEvent.List();
        return Ok(data);
    }

    [HttpGet("Edit/{adminCode}")]
    public async Task<IActionResult> Edit(string adminCode)
    {
        var data = await _blEvent.Edit(adminCode);
        return Ok(data);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(EventCreateRequestModel requestModel)
    {
        var data = await _blEvent.Create(requestModel);
        return Ok(data);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(EventUpdateRequestModel requestModel)
    {
        var data = await _blEvent.Update(requestModel);
        return Ok(data);
    }

    [HttpPost("Delete/{adminCode}")]
    public async Task<IActionResult> Delete(string adminCode)
    {
        var data = await _blEvent.Delete(adminCode);
        return Ok(data);
    }
}