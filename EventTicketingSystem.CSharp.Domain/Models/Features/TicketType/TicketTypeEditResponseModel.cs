namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;

public class TicketTypeEditResponseModel
{
    public TicketTypeEditModel? TicketType { get; set; }
}

public class TicketTypeEditModel
{
    public string? TicketTypeId { get; set; }
    public string? TicketTypeCode { get; set; }
    public string? EventCode { get; set; }
    public string? EventName { get; set; }
    public string? TicketTypeName { get; set; }
    public decimal Ticketprice { get; set; }
    public string? TicketQuantity { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public static TicketTypeEditModel FromTblTicketType (TblTickettype tblTickettype)
    {
        return new TicketTypeEditModel
        {
            TicketTypeId = tblTickettype.Tickettypeid,
            TicketTypeCode = tblTickettype.Tickettypecode,
            EventCode = tblTickettype.Eventcode,
            TicketTypeName = tblTickettype.Tickettypename,
            CreatedBy = tblTickettype.Createdby,
            CreatedAt = tblTickettype.Createdat,
            ModifiedBy = tblTickettype.Modifiedby,
            ModifiedAt = tblTickettype.Modifiedat,
        };
    }
}