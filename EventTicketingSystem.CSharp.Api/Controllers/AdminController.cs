using EventTicketingSystem.CSharp.Domain.Features.Admin;
using EventTicketingSystem.CSharp.Domain.Models.Features.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("Admin Users")]
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

    [HttpGet("GetAdminByCode/{adminCode}")]
    public async Task<IActionResult> GetAdminByCode(string adminCode)
    {
        var data = await _blAdmin.GetAdminByCode(adminCode);
        return Ok(data);
    }

    [HttpPost("CreateAdmin")]
    public async Task<IActionResult> CreateAdmin(AdminRequestModel requestModel)
    {
        var data = await _blAdmin.CreateAdmin(requestModel);
        return Ok(data);
    }

    [HttpPatch("UpdateAdmin/{adminCode}")]
    public async Task<IActionResult> UpdateAdmin (string adminCode, AdminRequestModel requestModel)
    {
        var data = await _blAdmin.UpdateAdmin(adminCode, requestModel);
        return Ok(data);
    }

    [HttpDelete("DeleteAdmin/{adminCode}")]
    public async Task<IActionResult> DeleteAdmin(string adminCode)
    {
        var data = await _blAdmin.DeleteAdminByCode(adminCode);
        return Ok(data);
    }
}