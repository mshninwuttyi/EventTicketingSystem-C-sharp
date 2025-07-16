using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueRequestModel
{
    public string VenueId { get; set; }           // Primary key
    public string VenueTypeCode { get; set; }
    public required string VenueName { get; set; }         // Name of the venue
    public string? VenueDescription { get; set; }         // Description
    public string? VenueAddress { get; set; }             // Address of the venue
    
    [Range(1, int.MaxValue)]
    public int? VenueCapacity { get; set; } 
    public string? VenueFacilities { get; set; }
    public string? VenueAddons { get; set; }
    public string? VenueImage { get; set; }  
    
    public bool DeleteFlag { get; set; }
}