namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueDeleteRequestModel
{
    [Required]
    public required string VenueCode { get; set; }
    
}