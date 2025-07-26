using EventTicketingSystem.CSharp.Domain.Features.VenueType;
using EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;

namespace EventTicketingSystem.CSharp.Api.Controllers
{
    [Tags("Venue Type")]
    [Route("api/[controller]")]
    [ApiController]
    public class VenueTypeController : ControllerBase
    {
        private readonly BL_VenueType _blVenueType;
        public VenueTypeController(BL_VenueType venueType)
        {
            _blVenueType = venueType;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            return Ok(await _blVenueType.List());
        }

        [HttpGet("Edit/{venueTypeCode}")]
        public async Task<IActionResult> Edit(string venueTypeCode)
        {
            return Ok(await _blVenueType.Edit(venueTypeCode));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] VenueTypeCreateRequestModel requestModel)
        {
            return Ok(await _blVenueType.Create(requestModel));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] VenueTypeUpdateRequestModel requestModel)
        {
            return Ok(await _blVenueType.Update(requestModel));
        }

        [HttpPost("Delete/{venueTypeCode}")]
        public async Task<IActionResult> Delete(string venueTypeCode)
        {
            return Ok(await _blVenueType.Delete(venueTypeCode));
        }
    }
}
