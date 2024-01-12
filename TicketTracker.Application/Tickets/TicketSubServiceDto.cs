using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketSubServiceDto
    {
        public int Id { get; set; }
        public string? SubServiceName { get; set; }
        public int DisplayOrderValue { get; set; }
    }
}
