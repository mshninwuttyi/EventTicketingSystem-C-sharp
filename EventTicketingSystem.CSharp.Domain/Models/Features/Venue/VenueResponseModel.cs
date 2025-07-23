namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueResponseModel
{
    public string VenueId { get; set; }           // Primary key

    public string VenueCode { get; set; }         // Code identifier

    public string VenueTypeCode { get; set; } 

    public string VenueName { get; set; }         // Name of the venue

    public string? Description { get; set; } // Description

    public string? Address { get; set; }     // Address of the venue

    public int? Capacity { get; set; }        // Capacity 

    public string? Facilities { get; set; }

    public string? Addons { get; set; }

    public string? Image { get; set; }        // Image filename or URL

    public bool DeleteFlag { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}