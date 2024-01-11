using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketService
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }

        //ef
        public List<TicketServiceConfiguration>? TicketServiceConfigurations { get; set; }
        public List<TicketSubService>? TicketSubServices { get; set; }
        public List<Ticket>? Tickets { get; set; }

    }
}
