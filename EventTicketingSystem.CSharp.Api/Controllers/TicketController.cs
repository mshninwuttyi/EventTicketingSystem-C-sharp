namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Ticket")]
[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly BL_Ticket _blTicket;

    public TicketController(BL_Ticket blTicket)
    {
        _blTicket = blTicket;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TicketRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest("Request model cannot be null.");
        }
        var result = await _blTicket.CreateTicket(requestModel);

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        var result = await _blTicket.GetAllTicket();

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }

    [HttpGet]
    [Route("/admin/Tickets")]
    public async Task<IActionResult> GetTicketList()
    {
       
        var result = await _blTicket.GetTicketList();
        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }
        return Ok(result.Data);
    }

    [HttpDelete("DeleteTicket/{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var result = await _blTicket.DeleteById(id);

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result);
    }


}