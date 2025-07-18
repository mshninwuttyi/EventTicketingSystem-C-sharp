namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Business Email")]
[Route("api/[controller]")]
[ApiController]
public class BusinessEmailController : ControllerBase
{
    private readonly BL_BusinessEmail _bl_BusinessEmail;

    public BusinessEmailController(BL_BusinessEmail bl_BusinessEmail)
    {
        _bl_BusinessEmail = bl_BusinessEmail;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BusinessEmailRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest("Request model cannot be null.");
        }
        var result = await _bl_BusinessEmail.Create(requestModel);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Data.BusinessEmailId }, result.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Business Email ID cannot be null or empty.");
        }

        var result = await _bl_BusinessEmail.GetById(id);

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        var result = await _bl_BusinessEmail.GetList();

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }
}