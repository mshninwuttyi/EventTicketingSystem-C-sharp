namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Verification Code")]
[Route("api/[controller]")]
[ApiController]
public class VerificationCodeController : ControllerBase
{
    private readonly BL_VerificationCode _vcService;

    public VerificationCodeController(BL_VerificationCode vcService)
    {
        _vcService = vcService;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        return Ok(await _vcService.List());
    }

    [HttpGet("Get/{vcId}")]
    public async Task<IActionResult> GetById(string vcId)
    {
        return Ok(await _vcService.GetById(vcId));
    }

    [HttpGet("GetByEmail/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        return Ok(await _vcService.GetByEmail(email));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] VCRequestModel requestModel)
    {
        return Ok(await _vcService.Create(requestModel));
    }

    [HttpPost("VerifyCode")]
    public async Task<IActionResult> VerifyCode([FromBody] VCRequestModel requestModel)
    {
        return Ok(await _vcService.VerifyCode(requestModel));
    }
}