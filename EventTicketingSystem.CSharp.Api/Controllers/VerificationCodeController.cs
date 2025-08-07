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
    [Authorize]
    public async Task<IActionResult> List()
    {
        return Ok(await _vcService.List());
    }

    [HttpGet("Get/{vcId}")]
    [Authorize]
    public async Task<IActionResult> GetById(string vcId)
    {
        return Ok(await _vcService.GetById(vcId));
    }

    [HttpGet("GetByEmail/{email}")]
    [Authorize]
    public async Task<IActionResult> GetByEmail(string email)
    {
        return Ok(await _vcService.GetByEmail(email));
    }

    [HttpPost("Create")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] VCRequestModel requestModel)
    {
        return Ok(await _vcService.Create(requestModel));
    }

    [HttpPost("VerifyCode")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode([FromBody] VCRequestModel requestModel)
    {
        return Ok(await _vcService.VerifyCode(requestModel));
    }
}