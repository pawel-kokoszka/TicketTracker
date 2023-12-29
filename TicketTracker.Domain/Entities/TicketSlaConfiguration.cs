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
        public int TicketTypeConfigurationId { get; set; }
        public int PriorityOrderValue { get; set; }
        public int TicketSlaId { get; set; }
        public string? Name {  get; set; }
        public string? Description { get; set; }


        //ef
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }
        
        public TicketSla? TicketSla { get; set; }

        
    }
}
