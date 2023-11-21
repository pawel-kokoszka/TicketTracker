using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketDetailsDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public string? DateCreated { get; set; } 

        public int PriorityId { get; set; }

        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
    }
}
