namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminListResponseModel
{
    public List<AdminListModel>? AdminList { get; set; }
}

public class AdminListModel
{
    public string? AdminId { get; set; }

    public string? AdminCode { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? Password { get; set; }

    public string? ProfileImage { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public static AdminListModel FromTblAdmin(TblAdmin admin)
    {
        return new AdminListModel
        {
            AdminId = admin.Adminid,
            AdminCode = admin.Admincode,
            Username = admin.Username,
            Email = admin.Email,
            PhoneNo = admin.Phoneno,
            ProfileImage = admin.Profileimage,
            Password = admin.Password,
            Createdby = admin.Createdby,
            Createdat = admin.Createdat
        };
    }
}