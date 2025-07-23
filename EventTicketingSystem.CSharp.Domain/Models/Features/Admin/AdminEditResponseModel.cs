namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminEditResponseModel
{
    public AdminEditModel? Admin { get; set; }
}

public class AdminEditModel
{
    public string? AdminId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? Password { get; set; }

    public string? ProfileImage { get; set; }

    public static AdminEditModel FromTblAdmin(TblAdmin admin)
    {
        return new AdminEditModel
        {
            AdminId = admin.Adminid,
            Username = admin.Username,
            Email = admin.Email,
            PhoneNo = admin.Phoneno,
            Password = admin.Password,
            ProfileImage = admin.Profileimage,
        };
    }
}