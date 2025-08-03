namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessEmail;

public class BusinessEmailEditResponseModel
{
    public BusinessEmailEditModel? BusinessEmail { get; set; }
}

public class BusinessEmailEditModel
{
    public string? BusinessEmailId { get; set; }
    public string? BusinessEmailCode { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public static BusinessEmailEditModel FromTblBusinessEmail(TblBusinessemail businessEmail)
    {
        return new BusinessEmailEditModel
        {
            BusinessEmailId = businessEmail.Businessemailid,
            BusinessEmailCode = businessEmail.Businessemailcode,
            FullName = businessEmail.Fullname,
            Phone = businessEmail.Phone,
            Email = businessEmail.Email,
        };
    }
}
