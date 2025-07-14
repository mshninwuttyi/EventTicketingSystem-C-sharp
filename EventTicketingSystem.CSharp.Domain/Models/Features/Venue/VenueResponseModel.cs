namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueResponseModel
{
    public string VenueId { get; set; }           // Primary key
    public string VenueCode { get; set; }         // Code identifier
    
    public string VenueTypeCode { get; set; } 
    public string VenueName { get; set; }         // Name of the venue
    public string? VenueDescription { get; set; } // Description (optional)
    public string? VenueAddress { get; set; }     // Address of the venue
    public int? VenueCapacity { get; set; }        // Capacity (nullable if unknown)
    public string? VenueFacilities { get; set; }
    public string? VenueAddons { get; set; }
    public string? VenueImage { get; set; }        // Image filename or URL
    
    public bool DeleteFlag { get; set; }
    
}