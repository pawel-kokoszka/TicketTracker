using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketPrioritiesGonfiguration
    {
        public int TicketTypeConfigurationId { get; set; }
        public int TicketPriorityId { get; set; }
        public int PriorityOrderValue { get; set; }
        public int TicketSlaConfigurationId { get; set; }

        //ef
        //public TicketPriority? TicketPriority { get; set; }
        //public TicketSlaConfiguration? TicketSlaConfiguration { get; set; }
        
    }
}
