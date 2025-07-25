using EventTicketingSystem.CSharp.Domain.Features.VenueType;
using EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;

namespace EventTicketingSystem.CSharp.Api.Controllers
{
    [Tags("Venue Type")]
    [Route("api/[controller]")]
    [ApiController]
    public class VenueTypeController : ControllerBase
    {
        private readonly BL_VenueType _venueType;

        public VenueTypeController(BL_VenueType venueType)
        {
            _venueType = venueType;
        }

        [HttpGet("GetVenueType")]
        public async Task<IActionResult> getVenueTypeList()
        {
            return Ok(await _venueType.getVenueTypeList());
        }

        [HttpPost("CreateVenueType")]
        public async Task<IActionResult> CreateVenueType([FromBody] VenueTypeRequestModel requestModel)
        {
            var response = await _venueType.CreateVenueType(requestModel);

            if (response.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
            return Ok(response);
        }

        [HttpPost("UpdateVenueType/{venueTypeCode}")]
        public async Task<IActionResult> UpdateVenueType(string venueTypeCode, [FromBody] VenueTypeRequestModel request)
        {
            request.VenueTypeCode = venueTypeCode;
            var response = await _venueType.UpdateVenueType(request);
            if (response.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
            return Ok(response);
        }

        [HttpPost("DeleteVenueType/{venueTypeCode}")]
        public async Task<IActionResult> DeleteVenueType(string venueTypeCode)
        {
            var request = new VenueTypeRequestModel();
            request.VenueTypeCode = venueTypeCode;
            var response = await _venueType.DeleteVenueType(request);
            if (response.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
            return Ok(response.Message);
        }
    }
}
