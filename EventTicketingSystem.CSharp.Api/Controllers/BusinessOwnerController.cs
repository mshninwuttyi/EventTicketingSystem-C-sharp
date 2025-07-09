using EventTicketingSystem.CSharp.Domain.Features.EventCategory;
using EventTicketingSystem.CSharp.Domain.Models.Features.EventCategory;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Business Owner")]
[Route("api/[controller]")]
[ApiController]
public class BusinessOwnerController : ControllerBase
{
    private readonly BL_BusinessOwner _blService;
    private readonly BL_EventCategory _blCategory;

    public BusinessOwnerController(BL_BusinessOwner blService, BL_EventCategory blCategory)
    {
        _blService = blService;
        _blCategory = blCategory;
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _blService.GetList());
    }
    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory([FromBody] EventCategoryRequestModel request)
    {
        var response = _blCategory.CreateCategory(request);
        return Ok(response);
    }


}