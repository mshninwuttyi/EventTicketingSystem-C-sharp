namespace EventTicketingSystem.CSharp.Api.Controller;

[Tags("Ticket Type")]
[Route("api/[controller]")]
[ApiController]
public class TicketTypeController : ControllerBase
{
    private readonly BL_TicketType _bL_TicketType;

    public TicketTypeController(BL_TicketType bL_TicketType)
    {
        _bL_TicketType = bL_TicketType;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        var lst = await _bL_TicketType.List();
        return Ok(lst);
    }

    [HttpGet("Edit/{code}")]
    public async Task<IActionResult> Edit(string code)
    {
        var ticketType = await _bL_TicketType.Edit(code);
        return Ok(ticketType);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(TicketTypeCreateRequestModel requestModel)
    {
        var result = await _bL_TicketType.Create(requestModel);
        return Ok(result);

    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(TicketTypeUpdateRequestModel requestModel)
    {
        var result = await _bL_TicketType.Update(requestModel);
        return Ok(result);
    }

    [HttpPost("Delete/{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        var ticketType = await _bL_TicketType.Delete(code);
        return Ok(ticketType);
    }
}