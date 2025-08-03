namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType
{
    public class TicketTypeListResponseModel
    {
        public List<TicketTypeListModel>? TicketTypeList { get; set; }
    }

public class TicketTypeListModel
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

        public static TicketTypeListModel FromTblTickettype(TblTickettype tblTickettype)
        {
            return new TicketTypeListModel
            {
                Tickettypecode = tblTickettype.Tickettypecode,
                Tickettypename = tblTickettype.Tickettypename,

            };

        }

    }
}