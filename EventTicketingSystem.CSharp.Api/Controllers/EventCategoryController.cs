using EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

namespace EventTicketingSystem.CSharp.Api.Controllers
{
    [Tags("Event Category")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        private readonly BL_EventCategory _blCategory;
        public EventCategoryController(BL_EventCategory blCategory) 
        { 
            _blCategory = blCategory;
        }
        #region Event Category

        [HttpGet("Get Category")]
        public async Task<IActionResult> GetCategoryList()
        {
            return Ok(await _blCategory.GetList());
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] EventCategoryRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return BadRequest("Request model cannot be null.");
            }
            var response = await _blCategory.CreateCategory(requestModel);

            if (response.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] EventCategoryRequestModel request)
        {
            return Ok(await _blCategory.UpdateCategory(request));
        }

        [HttpDelete("DeleteCategory/{categoryCode}")]
        public async Task<IActionResult> DeleteCategory(string categoryCode)
        {
            var request = new EventCategoryRequestModel { CategoryCode= categoryCode};
            return Ok(await _blCategory.DeleteCategory(request));
        }

        #endregion
    }
}
