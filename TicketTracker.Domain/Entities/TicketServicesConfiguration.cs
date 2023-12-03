using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketServicesConfiguration
    {
        public int TicketTypeConfigurationId {  get; set; }
        public int ServiceId { get; set; }

        //ef
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }
        public TicketService? TicketService { get; set; }

        
    }
}
