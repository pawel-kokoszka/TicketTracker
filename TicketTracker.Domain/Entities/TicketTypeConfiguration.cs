using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketTypeConfiguration
    {
        public int Id { get; set; }
        public int TicketTypeId { get; set; }
        public int ProjectConfigurationId { get; set; }

        //EF 
        public ProjectConfiguration? ProjectConfiguration { get; set; }
        public TicketType? TicketType { get; set; }
        public List<TicketSlaConfiguration>? TicketPrioritiesConfigurations { get; set; }
        public List<TicketFlowConfiguration>? TicketFlowConfigurations { get; set; }
        public List<TicketServiceConfiguration>? TicketServiceConfigurations { get; set; }
        public List<TicketTypeTeamAssignRule>? TicketTypeTeamAssignRules { get; set; }


    }
}
