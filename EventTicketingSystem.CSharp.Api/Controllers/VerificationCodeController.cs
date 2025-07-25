using EventTicketingSystem.CSharp.Domain.Features.VerificationCode;
using EventTicketingSystem.CSharp.Domain.Models.Features.VerificationCode;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Api.Controllers;

public class VerificationCodeController : Controller
{
    private readonly BL_VerificationCode _vcService;

    public VerificationCodeController(BL_VerificationCode vcService)
    {
        _vcService = vcService;
    }

    #region Create, Get List, Get By Id and Verify Code
    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _vcService.GetList());
    }

    [HttpGet("GetCode/{vcId}")]
    public async Task<IActionResult> GetById(string vcId)
    {
        var req = new VCRequestModel { Verificationid = vcId };
        return Ok(await _vcService.GetVCById(req));
    }

    [HttpPost("CreateNewVC")]
    public async Task<IActionResult> CreateNewVC([FromBody] VCRequestModel req)
    {
        return Ok(await _vcService.CreateNewVC(req));
    }

    [HttpPost("VerifyCode")]
    public async Task<IActionResult> VerifyCode([FromBody] VCRequestModel req)
    {
        return Ok(await _vcService.VerifyCode(req));
    }
    #endregion
}
