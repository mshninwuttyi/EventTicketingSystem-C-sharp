using EventTicketingSystem.CSharp.Domain.Models.Features.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.Ticket
{
    public class BL_Ticket
    {
        private readonly DA_Ticket _db;

        public BL_Ticket(DA_Ticket db)
        {
            _db = db;
        }
        public async Task<Result<List<TicketResponseModel>>> GetTicketList()
        {
            return await _db.GetTicketList();
        }
    }
}
