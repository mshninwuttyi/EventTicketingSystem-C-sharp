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

    [HttpGet("List")]
    [Authorize]
    public async Task<IActionResult> List()
    {
        var data = await _bl_BusinessEmail.List();
        return Ok(data);
    }

    [HttpGet("Edit/{businessEmailCode}")]
    [Authorize]
    public async Task<IActionResult> Edit(string businessEmailCode)
    {
        var data = await _bl_BusinessEmail.Edit(businessEmailCode);
        return Ok(data);
    }

    [HttpPost("Create")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] BusinessEmailCreateRequestModel requestModel)
    {
        var data = await _bl_BusinessEmail.Create(requestModel);
        return Ok(data);
    }
}