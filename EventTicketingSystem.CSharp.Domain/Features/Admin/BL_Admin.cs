using EventTicketingSystem.CSharp.Domain.Models.Features.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.Admin;

public class BL_Admin
{
    private readonly DA_Admin _dataAccess;

    public BL_Admin(DA_Admin dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<Result<AdminResponseModel>> GetAdminListAsync()
    {
        var data = await _dataAccess.GetAdminListAsync();
        return data;
    }

    public async Task<Result<AdminResponseModel>> GetAdminByCode(string adminCode)
    {
        var data = await _dataAccess.GetAdminByCodeAsync(adminCode);
        return data;
    }

    public async Task<Result<AdminResponseModel>> CreateAdmin (AdminRequestModel requestModel)
    {
        var data = await _dataAccess.CreateAdminAsync(requestModel);
        return data;
    }

    public async Task<Result<AdminResponseModel>> UpdateAdmin (string adminCode, AdminRequestModel requestModel)
    {
        var data = await _dataAccess.UpdateAdminAsync(adminCode, requestModel);
        return data;
    }

    public async Task<Result<AdminResponseModel>> DeleteAdminByCode(string adminCode)
    {
        var data = await _dataAccess.DeleteAdminByCode(adminCode);
        return data;
    }
}
