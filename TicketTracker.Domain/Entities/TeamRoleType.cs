using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TeamRoleType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        //ef
        public List<TeamRole>? TeamsRoles { get; set; }
    }
}
