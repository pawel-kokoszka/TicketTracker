using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketPrioritiesGonfiguration
    {
        public int ProjectConfigurationId { get; set; }
        public int TicketPriorityId { get; set; }
        public int PriorityOrderValue { get; set; }
        public int SlaId { get; set; }
    }
}
