namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerListResponseModel
{
    public List<BusinessOwnerListModel>? BusinessOwners { get; set; }
}

public class BusinessOwnerListModel
{
    public string? Businessownerid { get; set; }

    public string? Businessownercode { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public static BusinessOwnerListModel FromTblOwner(TblBusinessowner owner)
    {
        return new BusinessOwnerListModel
        {
            Businessownerid = owner.Businessownerid,
            Businessownercode = owner.Businessownercode,
            FullName = owner.Fullname,
            Email = owner.Email,
            Phone = owner.Phone,
            Createdby = owner.Createdby,
            Createdat = owner.Createdat,
            Modifiedat = owner.Modifiedat,
            Modifiedby = owner.Modifiedby,
            Deleteflag = owner.Deleteflag,
        };
    }
}