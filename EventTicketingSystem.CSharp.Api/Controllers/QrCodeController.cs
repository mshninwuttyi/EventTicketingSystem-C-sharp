namespace EventTicketingSystem.CSharp.Api.Controllers;

[Tags("QR Code")]
[Route("api/[controller]")]
[ApiController]
public class QrCodeController : ControllerBase
{
    private readonly BL_QrCode _bl_QrCode;

    public QrCodeController(BL_QrCode bl_QrCode)
    {
        _bl_QrCode = bl_QrCode;
    }

    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] QrGenerateRequestModel requestModel)
    {
        if (requestModel == null)
        {
            return BadRequest("Request model cannot be null.");
        }

        var result = await _bl_QrCode.Generate(requestModel);

        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }

        return Ok(result.Data);
    }

    [HttpGet("{qrString}")]
    public async Task<IActionResult> Check(string qrString)
    {
        if (string.IsNullOrEmpty(qrString))
        {
            return BadRequest("Request model cannot be null.");
        }

        var result = await _bl_QrCode.Check(qrString);
        if (result.IsError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
        }
        return Ok(result.Data);
    }
}