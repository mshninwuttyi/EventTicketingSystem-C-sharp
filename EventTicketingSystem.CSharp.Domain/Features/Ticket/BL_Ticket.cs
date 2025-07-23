namespace EventTicketingSystem.CSharp.Domain.Features.Ticket;

public class BL_Ticket
{
    private readonly DA_Ticket _da_Ticket;

    public BL_Ticket(DA_Ticket da_Ticket)
    {
        _da_Ticket = da_Ticket;
    }

    public async Task<Result<TicketResponseModel>> CreateTicket(TicketRequestModel requestModel)
    {
        return await _da_Ticket.CreateTicket(requestModel);
    }

    public async Task<Result<TicketListResponseModel>> GetAllTicket()
    {
        return await _da_Ticket.GetAllTicket();
    }

    public async Task<Result<List<TicketResponseModel>>> GetTicketList()
    {
        return await _da_Ticket.GetTicketList();
    }
}