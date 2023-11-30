using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketStatesGonfigurations
    {
        public int ProjectConfigurationId { get; set; }
        public int TicketStateId { get; set; }
        public int TicketStateOrderValue { get; set; }

    }
}
