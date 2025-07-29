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

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] BusinessEmailCreateRequestModel requestModel)
    {
        var data = await _bl_BusinessEmail.Create(requestModel);
        return Ok(data);
    }

    [HttpGet("Edit/{businessEmailCode}")]
    public async Task<IActionResult> Edit(string businessEmailCode)
    {
        var data = await _bl_BusinessEmail.Edit(businessEmailCode);
        return Ok(data);
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