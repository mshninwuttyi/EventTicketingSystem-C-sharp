using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType
{
    public class TicketTypeEditResponseModel
    {
        public TicketTypeEditModel? ticketTypeEditModel { get; set; }
    }
    public class TicketTypeEditModel
    {
        public string Tickettypeid { get; set; } 

        public string Tickettypecode { get; set; }

        public string Tickettypename { get; set; } 

        public string Createdby { get; set; }

        public DateTime Createdat { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifiedat { get; set; }

        public static TicketTypeEditModel FromTblTickettypes(TblTickettype tickettypes)
        {
            return new TicketTypeEditModel
            {
                Tickettypeid = tickettypes.Tickettypeid,
                Tickettypecode = tickettypes.Tickettypecode,
                //Eventcode  
                Tickettypename = tickettypes.Tickettypename,
                Createdby = tickettypes.Createdby,
                Createdat = tickettypes.Createdat,
                Modifiedby = tickettypes.Modifiedby,    
                Modifiedat = tickettypes.Modifiedat,

            };
        }
    }

}
