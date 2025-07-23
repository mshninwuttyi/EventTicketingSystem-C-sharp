namespace EventTicketingSystem.CSharp.Domain.Models.Features.VerificationCode;

public class VCResponseModel
{
    public List<VCodeModel> VerificationCodes {  get; set; }

    public VCodeModel VerificationCode { get; set; }
}

public class VCodeModel{
    public string? Verificationid { get; set; }

    public string? Verificationcode { get; set; }

    public string? Email { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public static VCodeModel FromTblVerification(TblVerification verification)
    {
        return new VCodeModel
        {
            Verificationid = verification.Verificationid,
            Verificationcode = verification.Verificationcode,
            Email = verification.Email,
            Createdby = verification.Createdby,
            Createdat = verification.Createdat,
            Modifiedat = verification.Modifiedat,
            Modifiedby = verification.Modifiedby,
            Deleteflag = verification.Deleteflag,
        };
    }
}