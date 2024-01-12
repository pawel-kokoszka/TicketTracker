using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets
{
    public class TicketServiceDto
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }

        public List<TicketSubServiceDto>? TicketSubServices { get; set; } 


    }
}
