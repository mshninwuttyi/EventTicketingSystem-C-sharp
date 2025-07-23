namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminResponseModel
{
    public List<AdminModel> AdminList { get; set; }
    public AdminModel Admin { get; set; }
}

public class AdminModel
{
    public string? AdminId { get; set; }

    public string? AdminCode { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }
    
    public string? PhoneNo { get; set; }

    public string? Password { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public static AdminModel FromTblAdmin(TblAdmin admin)
    {
        return new AdminModel
        {
            AdminId = admin.Adminid,
            AdminCode = admin.Admincode,
            Username = admin.Username,
            Email = admin.Email,
            PhoneNo = admin.Phoneno,
            Password = admin.Password,
            Createdby = admin.Createdby,
            Createdat = admin.Createdat,
            Modifiedby = admin.Modifiedby,
            Modifiedat = admin.Modifiedat,
            Deleteflag = admin.Deleteflag,
        };
    }
}