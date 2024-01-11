using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketServiceConfiguration
    {
        public int TicketTypeConfigurationId {  get; set; }
        public int TicketServiceId { get; set; }
        public int DisplayOrderValue { get; set; }

        //ef
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }
        public TicketService? TicketService { get; set; }

        
    }
}
