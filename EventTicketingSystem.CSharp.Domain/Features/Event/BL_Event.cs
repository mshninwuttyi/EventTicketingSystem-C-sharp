namespace EventTicketingSystem.CSharp.Domain.Features.Event;

public class BL_Event
{
    private readonly DA_Event _daService;

    public BL_Event(DA_Event service)
    {
        _daService = service;
    }

    public async Task<Result<EventListResponseModel>> GetEventList()
    {
        return await _daService.GetEventList();
    }

    public async Task<Result<EventResponseModel>> EventDetail(string? EventCode)
    {
        return await _daService.EventDetail(EventCode);
    }

    public async Task<Result<EventResponseModel>> CreateEvent(EventRequestModel requestModel)
    {
        return await _daService.CreateEvent(requestModel);
    }

    public async Task<Result<EventResponseModel>> UpdateEvent(EventRequestModel requestModel)
    {
        return await _daService.UpdateEvent(requestModel);
    }

    public async Task<Result<EventResponseModel>> DeleteEvent(string EventCode)
    {
        return await _daService.DeleteEvent(EventCode);
    }
}