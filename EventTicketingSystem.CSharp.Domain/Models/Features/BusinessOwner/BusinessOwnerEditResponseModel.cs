namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerEditResponseModel
{
    public BusinessOwnerEditModel? BusinessOwner { get; set; }
}

public class BusinessOwnerEditModel
{
    public string? Businessownercode { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public static BusinessOwnerEditModel FromTblOwner(TblBusinessowner owner)
    {
        return new BusinessOwnerEditModel
        {
            Businessownercode = owner.Businessownercode,
            FullName = owner.Fullname,
            Email = owner.Email,
            Phone = owner.Phone
        };
    }
}