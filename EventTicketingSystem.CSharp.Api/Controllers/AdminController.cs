namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Admin User")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly BL_Admin _blAdmin;

    public AdminController(BL_Admin blAdmin)
    {
        _blAdmin = blAdmin;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List()
    {
        var data = await _blAdmin.List();
        return Ok(data);
    }

    [HttpGet("Edit/{adminCode}")]
    public async Task<IActionResult> Edit(string adminCode)
    {
        var data = await _blAdmin.Edit(adminCode);
        return Ok(data);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(AdminCreateRequestModel requestModel)
    {
        var data = await _blAdmin.Create(requestModel);
        return Ok(data);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(AdminUpdateRequestModel requestModel)
    {
        var data = await _blAdmin.Update(requestModel);
        return Ok(data);
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(AdminDeleteRequestModel requestModel)
    {
        var data = await _blAdmin.Delete(requestModel);
        return Ok(data);
    }
}