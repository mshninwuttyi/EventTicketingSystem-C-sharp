using EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Business Owner")]
[Route("api/[controller]")]
[ApiController]
public class BusinessOwnerController : ControllerBase
{
    private readonly BL_BusinessOwner _blService;


    public BusinessOwnerController(BL_BusinessOwner blService)
    {
        _blService = blService;
    }

    #region CURD Business Owner
    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _blService.GetList());
    }

    [HttpGet("GetOwnerByCode/{ownerCode}")]
    public async Task<IActionResult> GetByCode(string ownerCode)
    {
        var req = new BusinessOwnerRequestModel { Businessownercode = ownerCode };
        return Ok(await _blService.GetBusinessOwner(req));
    }

    [HttpPost("CreateOwner")]
    public async Task<IActionResult> CreateOwner([FromBody] BusinessOwnerRequestModel req)
    {
        return Ok(await _blService.CreateNewBusinessOwner(req));
    }

    [HttpPatch("GetBOwnerByCode")]
    public async Task<IActionResult> UpdateOwner([FromBody] BusinessOwnerRequestModel req)
    {
        return Ok(await _blService.UpdateBusinessOwner(req));
    }

    [HttpDelete("DeleteOwner/{ownerCode}")]
    public async Task<IActionResult> DeleteByCode(string ownerCode)
    {
        var req = new BusinessOwnerRequestModel { Businessownercode = ownerCode };
        return Ok(await _blService.GetBusinessOwner(req));
    }
    #endregion
}