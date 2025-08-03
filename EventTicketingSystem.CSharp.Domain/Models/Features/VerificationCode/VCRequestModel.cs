namespace EventTicketingSystem.CSharp.Domain.Models.Features.VerificationCode;

public class VCRequestModel
{
    public string? VerificationId { get; set; }

    public string? VerificationCode { get; set; }

    public string? Email { get; set; }

    public DateTime? ExpiredTime { get; set; }

    public bool? Isused { get; set; }

    public string? Admin { get; set; }

    public bool? Deleteflag { get; set; }
}