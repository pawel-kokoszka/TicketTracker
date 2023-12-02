using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketSlaConfiguration
    {
        public int Id { get; set; } 
        public int TicketSlaConfigurationId {  get; set; }
        public int TicketSlaId { get; set; }

        //ef
        public TicketSla? TicketSla { get; set; }
        public TicketPrioritiesGonfiguration? TicketPrioritiesGonfiguration { get; set; }
        
        
    }
}
