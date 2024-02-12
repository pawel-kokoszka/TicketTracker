using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.TicketRoles
{
    public class TicketRole
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }

    public class TicketRoles
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public Dictionary<string ,bool > roles { get; set; }

    }
}
