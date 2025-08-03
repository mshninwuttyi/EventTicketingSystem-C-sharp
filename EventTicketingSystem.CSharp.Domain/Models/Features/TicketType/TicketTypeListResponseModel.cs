using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;

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

    public static TicketTypeEditModel FromTblTicketType(TblTickettype tblTickettype)
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
