using EventTicketingSystem.CSharp.Domain.Features.Admin;
using EventTicketingSystem.CSharp.Domain.Models.Features.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly BL_Admin _blAdmin;

    public AdminController(BL_Admin blAdmin)
    {
        _blAdmin = blAdmin;
    }

    [HttpGet("GetAdminList")]
    public async Task<IActionResult> GetAdminList()
    {
        var data = await _blAdmin.GetAdminListAsync();
        return Ok(data);
    }

    [HttpGet("GetAdminByCode/{admincode}")]
    public async Task<IActionResult> GetAdminByCode(string admincode)
    {
        var data = await _blAdmin.GetAdminByCode(admincode);
        return Ok(data);
    }

    [HttpPost("CreateAdmin")]
    public async Task<IActionResult> CreateAdmin(AdminRequestModel requestModel)
    {
        var data = await _blAdmin.CreateAdmin(requestModel);
        return Ok(data);
    }

    [HttpPatch("UpdateAdmin/{admincode}")]
    public async Task<IActionResult> UpdateAdmin (string admincode, AdminRequestModel requestModel)
    {
        var data = await _blAdmin.UpdateAdmin(admincode, requestModel);
        return Ok(data);
    }

    [HttpDelete("DeleteAdmin/{admincode}")]
    public async Task<IActionResult> DeleteAdmin(string admincode)
    {
        var data = await _blAdmin.DeleteAdminByCode(admincode);
        return Ok(data);
    }
}