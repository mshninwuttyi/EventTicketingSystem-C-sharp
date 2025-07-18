namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerResponseModel
{
    public List<BusinessOwnerModel> BusinessOwners { get; set; }

    public BusinessOwnerModel BusinessOwner { get; set; }
}

public class BusinessOwnerModel
{
    public string? Businessownerid { get; set; }

    public string? Businessownercode { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public static BusinessOwnerModel FromTblOwner(TblBusinessowner owner)
    {
        return new BusinessOwnerModel
        {
            Businessownerid = owner.Businessownerid,
            Businessownercode = owner.Businessownercode,
            Name = owner.Name,
            Email = owner.Email,
            Phonenumber = owner.Phonenumber,
            Createdby = owner.Createdby,
            Createdat = owner.Createdat,
            Modifiedat = owner.Modifiedat,
            Modifiedby = owner.Modifiedby,
            Deleteflag = owner.Deleteflag,
        };
    }
}