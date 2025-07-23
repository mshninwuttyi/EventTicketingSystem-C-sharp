using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin
{
    public class AdminRequestModel
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PhoneNo { get; set; }

        public string? Password { get; set; }
        public string? Admin { get; set; }

    }
}
