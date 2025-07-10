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
        public async Task<IActionResult> CreateCategory([FromBody] EventCategoryRequestModel request)
        {
            var response = _blCategory.CreateCategory(request);
            return Ok(await response);
        }


        #endregion
    }
}
