namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueUpdateRequestModel
{
    [Required]
    public required string VenueId { get; set; }  
    
    public string? Address { get; set; }             

    public string? Description { get; set; }         

    public string? Facilities { get; set; }

    public string? Addons { get; set; }

}