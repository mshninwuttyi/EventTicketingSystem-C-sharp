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

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        return Ok(await _blBusinessOwner.List());
    }

    [HttpGet("Edit/{ownerCode}")]
    public async Task<IActionResult> Edit(string ownerCode)
    {
        return Ok(await _blBusinessOwner.Edit(ownerCode));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] BusinessOwnerCreateRequestModel requestModel)
    {
        return Ok(await _blBusinessOwner.Create(requestModel));
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] BusinessOwnerUpdateRequestModel requestModel)
    {
        return Ok(await _blBusinessOwner.Update(requestModel));
    }

    [HttpPost("Delete/{ownerCode}")]
    public async Task<IActionResult> Delete(string ownerCode)
    {
        return Ok(await _blBusinessOwner.Delete(ownerCode));
    }
}