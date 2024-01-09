using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketFlowConfiguration
    {
        public int Id { get; set; }
        public int TicketTypeConfigurationId { get; set; }
        public int StatusId { get; set; }
        public int NextStatusId { get; set; }

        //EF
        public TicketStatus? TicketStatuses { get; set; }
        public TicketTypeConfiguration? TicketTypeConfigurations { get; set; }
    }
}
