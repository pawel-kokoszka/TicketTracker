using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketSlaDto
    {
        public int Id { get; set; }
        public int TicketTypeConfigurationId { get; set; }
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfMinutes { get; set; }
    }
}
