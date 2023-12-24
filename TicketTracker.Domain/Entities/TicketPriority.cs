using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketPriority
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //EF
        public List<Ticket>? Tickets { get; set; }
        public List<TicketPrioritiesConfiguration>? TicketPrioritiesConfigurations { get; set; }

    }
}
