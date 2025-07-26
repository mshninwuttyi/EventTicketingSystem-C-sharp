using EventTicketingSystem.CSharp.Domain.Features.TicketType;
using EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        private readonly BL_TicketType _bL_TicketType;

        public TicketTypeController(BL_TicketType bL_TicketType)
        {
            _bL_TicketType = bL_TicketType;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetTicketTypeList()
        {
            var lst = await _bL_TicketType.getTicketTypeList();
            return Ok(lst);
        }

        [HttpGet("Edit/{code}")]
        public async Task<IActionResult> GetTicketTypeByCode(string code)
        {
            var ticketType = await _bL_TicketType.getTicketTypeByCode(code);
            return Ok(ticketType);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTicketType(TicketTypeCreateRequestModel requestModel)
        {
            var result = await _bL_TicketType.createTicketType(requestModel);
            return Ok(result);

        }

        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateTicketType(TicketTypeUpdateRequestModel requestModel)
        {
            var result = await _bL_TicketType.updateTicketType(requestModel);
            return Ok(result);
        }


        [HttpDelete("Delete/{code}")]
        public async Task<IActionResult> DeleteTicketType(string code)
        {
            var ticketType = await _bL_TicketType.deleteTickeType(code);
            return Ok(ticketType);
        }
    }
}
