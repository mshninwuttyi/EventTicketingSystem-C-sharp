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

        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] EventCategoryRequestModel request)
        {
            return Ok(await _blCategory.UpdateCategory(request));
        }

        [HttpPost("CreateCategory1")]
        public async Task<IActionResult> CreateCategory(string categoryName, string adminName)
        {
            var request = new EventCategoryRequestModel
            {
                CategoryName = categoryName,
                AdminName = adminName
            };

            var response = _blCategory.CreateCategory(request);
            return Ok(await response);
        }

        [HttpPost("UpdateCategory1")]
        public async Task<IActionResult> UpdateCategory(string categoryName, string adminName, string categoryCode)
        {
            var request = new EventCategoryRequestModel
            {
                CategoryCode = categoryCode,
                CategoryName = categoryName,
                AdminName = adminName
            };

            var response = _blCategory.UpdateCategory(request);
            
            return Ok(await response);
        }

        #endregion
    }
}
