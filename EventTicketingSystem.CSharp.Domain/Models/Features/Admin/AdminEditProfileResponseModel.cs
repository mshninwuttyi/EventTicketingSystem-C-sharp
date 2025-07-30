namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminEditProfileResponseModel
{
    public AdminEditProfileModel Admin { get; set; } = new();
}

public class AdminEditProfileModel
{
    public string? AdminCode { get; set; }

    public string? Username { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? ProfileImage { get; set; }

    public static AdminEditProfileModel FromTblAdmin(TblAdmin admin)
    {
        return new AdminEditProfileModel
        {
            AdminCode = admin.Admincode,
            Username = admin.Username,
            Email = admin.Email,
            PhoneNo = admin.Phone,
            FullName = admin.Fullname,
            ProfileImage = admin.Profileimage,
        };
    }
}