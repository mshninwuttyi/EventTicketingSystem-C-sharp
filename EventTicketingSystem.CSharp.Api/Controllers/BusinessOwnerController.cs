namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Business Owner")]
[Route("api/[controller]")]
[ApiController]
public class BusinessOwnerController : ControllerBase
{
    private readonly BL_BusinessOwner _blBusinessOwner;

    public BusinessOwnerController(BL_BusinessOwner blBusinessOwner)
    {
        _blBusinessOwner = blBusinessOwner;
    }

    #region CURD Business Owner

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _blBusinessOwner.GetList());
    }

    [HttpGet("GetOwnerByCode/{ownerCode}")]
    public async Task<IActionResult> GetByCode(string ownerCode)
    {
        var req = new BusinessOwnerRequestModel { Businessownercode = ownerCode };
        return Ok(await _blBusinessOwner.GetBusinessOwner(req));
    }

    [HttpPost("CreateOwner")]
    public async Task<IActionResult> CreateOwner([FromBody] BusinessOwnerRequestModel req)
    {
        return Ok(await _blBusinessOwner.CreateNewBusinessOwner(req));
    }

    [HttpPatch("GetBOwnerByCode")]
    public async Task<IActionResult> UpdateOwner([FromBody] BusinessOwnerRequestModel req)
    {
        return Ok(await _blBusinessOwner.UpdateBusinessOwner(req));
    }

    [HttpDelete("DeleteOwner/{ownerCode}")]
    public async Task<IActionResult> DeleteByCode(string ownerCode)
    {
        var req = new BusinessOwnerRequestModel { Businessownercode = ownerCode };
        return Ok(await _blBusinessOwner.GetBusinessOwner(req));
    }

    #endregion
}