using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketType
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }

        //EF
        public List<Ticket>? Tickets { get; set; } 
        public List<TicketTypeConfiguration>? TicketTypeConfiguration { get; set; } 

    }
}
