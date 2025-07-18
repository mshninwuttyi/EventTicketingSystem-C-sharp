namespace EventTicketingSystem.CSharp.Shared.Enums;

public enum EnumTableUniqueName
{
    [Description("AD")]
    Tbl_Admin,

    [Description("EC")]
    Tbl_EventCategory,

    [Description("BO")]
    Tbl_BusinessOwner,

    [Description("TI")]
    Tbl_Ticket,

    [Description("TP")]
    Tbl_TicketPrice,

    [Description("TT")]
    Tbl_TicketType,

    [Description("TR")]
    Tbl_Transaction,

    [Description("TC")]
    Tbl_TransactionTicket,

    [Description("BE")]
    Tbl_BusinessEmail,

    [Description("EV")]
    Tbl_Event,

    [Description("VC")]
    Tbl_Verification,

    [Description("VE")]
    Tbl_Venue,

    [Description("VT")]
    Tbl_VenueType,
}