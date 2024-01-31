using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public int TeamTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsExternal { get; set; }
        public bool IsActive { get; set; }
        public int SeniorityLevel { get; set; }

        //ef
        public List<TeamsUsers>? TeamsUsers {  get; set; }
        public TeamType? TeamType { get; set; } 

        public List<TicketTypeTeamAssigningRule>? TicketTypeTeamAssigningRules { get; set; }

        public List<TeamRole>? TeamRoles { get; set; }

    }
}
