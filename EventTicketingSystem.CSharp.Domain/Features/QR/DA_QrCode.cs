using EventTicketingSystem.CSharp.Domain.Models.Features.QR;

namespace EventTicketingSystem.CSharp.Domain.Features.QR;

public class DA_QrCode
{
    private readonly ILogger<DA_QrCode> _logger;
    private readonly AppDbContext _db;

    public DA_QrCode(ILogger<DA_QrCode> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Result<QrGenerateResponseModel>> GenerateQr(QrGenerateRequestModel requestModel)
    {
        var response = new QrGenerateResponseModel();

        string ticketTypeCode = requestModel.TicketTypeCode;
        string ticketPriceCode = requestModel.TicketPriceCode;
        string ticketCode = requestModel.TicketCode;
        string eventCode = requestModel.EventCode;

        var tblTicketType = await _db.TblTickettypes.FirstOrDefaultAsync(x => x.Tickettypecode == ticketTypeCode && x.Deleteflag == false);
        var tblTicketPriceCode = await _db.TblTicketprices.FirstOrDefaultAsync(x => x.Ticketpricecode == ticketPriceCode && x.Deleteflag == false);
        var tblTicketCode = await _db.TblTickets.FirstOrDefaultAsync(x => x.Ticketcode == ticketCode && x.Deleteflag == false);
        var tblEvent = await _db.TblEvents.FirstOrDefaultAsync(x => x.Eventcode == eventCode && x.Deleteflag == false);

        if (tblTicketType == null || tblTicketPriceCode == null || tblTicketCode == null || tblEvent == null)
        {
            _logger.LogError("Invalid data provided for QR code generation.");
            return Result<QrGenerateResponseModel>.SystemError("Invalid data provided for QR code generation.");
        }

        //string GateOpenTime = "GateOpenTime";
        //string VenueName = "VenueName";

        string qrString = $"{tblEvent.Eventname}" +
            $"|{tblEvent.Eventcode}" +
            $"|{DateOnly.FromDateTime((DateTime)tblEvent.Startdate)}" +
            $"|{tblEvent.Startdate}" +
            $"|{tblEvent.Enddate}" +
            $"|GateOpenTime" +
            $"|{tblTicketCode.Ticketcode}" +
            $"|{tblTicketPriceCode.Ticketprice}" +
            $"|{tblTicketType.Tickettypename}" +
            $"|{requestModel.FullName}" +
            $"|{requestModel.Email}" +
            $"|VenueName";

        response.QrString = qrString;

        return Result<QrGenerateResponseModel>.Success(response, "QR code generated successfully.");

    }

    public Result<QrCheckResponseModel> CheckQr(string qrString)
    {
        var response = new QrCheckResponseModel();

        if (string.IsNullOrEmpty(qrString))
        {
            _logger.LogError("QR string cannot be null or empty.");
            return Result<QrCheckResponseModel>.SystemError("QR string cannot be null or empty.");
        }

        var qrParts = qrString.Split('|');
        if (qrParts.Length < 10)
        {
            _logger.LogError("Invalid QR string format.");
            return Result<QrCheckResponseModel>.SystemError("Invalid QR string format.");
        }

        response.EventName = qrParts[0];
        response.EventCode = qrParts[1];
        response.EventDate = qrParts[2];
        response.EventTimeFrom = qrParts[3];
        response.EventTimeTo = qrParts[4];
        response.GateOpenTime = qrParts[5];
        response.TicketCode = qrParts[6];
        response.TicketPrice = qrParts[7];
        response.TicketType = qrParts[8];
        response.FullName = qrParts[9];
        response.Email = qrParts[10];
        response.Location = qrParts.Length > 11 ? qrParts[11] : string.Empty;
        response.Address = qrParts.Length > 12 ? qrParts[12] : string.Empty;

        return Result<QrCheckResponseModel>.Success(response, "QR code is valid.");
    }
}
