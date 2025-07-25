namespace EventTicketingSystem.CSharp.Domain.Models.Features.VerificationCode;

public class VCRequestModel
{
    public string? Verificationid { get; set; }

    public string? Verificationcode { get; set; }

    public string? Email { get; set; }

    public string? Admin { get; set; }

    public bool? Deleteflag { get; set; }
}