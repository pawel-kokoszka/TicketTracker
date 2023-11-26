using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets
{
    public class CreateTicketViewRequiredDto
    {
        public IEnumerable<TicketPriority>? TicketPriorities { get; set; }
        public IEnumerable<TicketType>? TicketTypes { get; set; } 

        public TicketCreateDto? CreateDto { get; set; }  
    }
}
