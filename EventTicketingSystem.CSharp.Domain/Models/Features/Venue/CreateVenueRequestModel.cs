namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class CreateVenueRequestModel
{
    [Required]
    public required string VenueTypeCode { get; set; }

    [Required]
    public required string VenueName { get; set; }         // Name of the venue

    public string? Description { get; set; }         // Description

    public string? Address { get; set; }             // Address of the venue

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; } 

    public string? Facilities { get; set; }

    public string? Addons { get; set; }

    public string? Image { get; set; }  
}