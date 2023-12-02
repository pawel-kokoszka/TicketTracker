using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketStatesGonfiguration
    {
        public int TicketTypeConfigurationId { get; set; }
        public int TicketStateId { get; set; }
        public int TicketStateOrderValue { get; set; }

        //ef
        public TicketState? TicketState { get; set; }
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }

    }
}
