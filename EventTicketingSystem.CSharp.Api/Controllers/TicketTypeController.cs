namespace EventTicketingSystem.CSharp.Api.Controllers;

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
    [AllowAnonymous]
    public async Task<IActionResult> List()
    {
        var lst = await _bL_TicketType.List();
        return Ok(lst);
    }

    [HttpGet("Edit/{ticketTypeCode}")]
    [AllowAnonymous]
    public async Task<IActionResult> Edit(string ticketTypeCode)
    {
        var ticketType = await _bL_TicketType.Edit(ticketTypeCode);
        return Ok(ticketType);
    }

    [HttpPost("Create")]
    [Authorize]
    public async Task<IActionResult> Create(TicketTypeCreateRequestModel requestModel)
    {
        var result = await _bL_TicketType.Create(requestModel);
        return Ok(result);

    }

    [HttpPost("Update")]
    [Authorize]
    public async Task<IActionResult> Update(TicketTypeUpdateRequestModel requestModel)
    {
        var result = await _bL_TicketType.Update(requestModel);
        return Ok(result);
    }

    [HttpPost("Delete/{ticketTypeCode}")]
    [Authorize]
    public async Task<IActionResult> Delete(string ticketTypeCode)
    {
        var ticketType = await _bL_TicketType.Delete(ticketTypeCode);
        return Ok(ticketType);
    }
}