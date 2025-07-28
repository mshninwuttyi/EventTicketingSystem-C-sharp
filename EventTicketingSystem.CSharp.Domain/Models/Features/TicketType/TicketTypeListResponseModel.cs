namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType
{
    public class TicketTypeListResponseModel
    {
        public List<TicketTypeListModel>? TicketTypeList { get; set; }
    }

    public class TicketTypeListModel
    {
        public string? Tickettypecode { get; set; }

        public string? Tickettypename { get; set; }

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