using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketState
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    
        //ef
        public TicketStatesGonfiguration? TicketStatesGonfiguration { get; set; }

    }

}
