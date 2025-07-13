using EventTicketingSystem.CSharp.Domain.Features.Ticket;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly BL_Ticket _blService;
        public TicketController(BL_Ticket blService)
        {
            _blService = blService;
        }
        [HttpGet]
        [Route("/admin/Tickets")]
        public async Task<IActionResult> GetTicketList()
        {
            try
            {
                var result = await _blService.GetTicketList();
                if (result.IsError)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
                }
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
