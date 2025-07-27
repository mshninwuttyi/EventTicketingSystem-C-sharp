namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueCreateRequestModel
{
    [Required]
    public required string VenueTypeCode { get; set; }

    [Required]
    public required string VenueName { get; set; }         // Name of the venue
    
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
    
    public string? Address { get; set; }             // Address of the venue

    public string? Description { get; set; }         // Description
    
    public string? Facilities { get; set; }
    
    public List<string>? Addons { get; set; }

    public List<IFormFile> VenueImage { get; set; }
}