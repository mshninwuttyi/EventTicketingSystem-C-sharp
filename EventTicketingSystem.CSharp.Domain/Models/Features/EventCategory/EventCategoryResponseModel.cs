namespace EventTicketingSystem.CSharp.Domain.Features.EventCategory;

public class EventCategoryResponseModel
{
    public List<EventCategoryModel> EventCategories { get; set; }

}

public class EventCategoryModel
{
    public string? EventCategoryid { get; set; }

    public string? EventCategorycode { get; set; }

    public string? Categoryname { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }


}