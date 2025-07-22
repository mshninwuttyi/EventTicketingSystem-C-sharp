using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTicketingSystem.CSharp.Domain.Models.Features.Event;

namespace EventTicketingSystem.CSharp.Domain.Features.Event
{
    public class BL_Event
    {
        private readonly BL_Event _service;

        public BL_Event(BL_Event service)
        {
            _service = service;
        }

        public async Task<Result<EventListResponseModel>> GetEventList()
        {
            return await _service.GetEventList();
        }

        public async Task<Result<EventResponseModel>> EventDetial(string? EventCode)
        {
            return await _service.EventDetial(EventCode);
        }

        public async Task<Result<EventResponseModel>> CreateEvent(EventRequestModel requestModel)
        {
            return await _service.CreateEvent(requestModel);
        }

        public async Task<Result<EventResponseModel>> UpdateEvent(EventRequestModel requestModel)
        {
            return await _service.UpdateEvent(requestModel);
        }

        public async Task<Result<EventResponseModel>> DeleteEvent(string EventCode, string Admin)
        {
            return await _service.DeleteEvent(EventCode, Admin);
        }
    }
}
