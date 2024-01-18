using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class UserTeamConfiguration
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        
        public int TeamTypeId { get; set; }
        public string? Description { get; set; }

        public string? UserId { get; set; }

        public int  displayOrder { get; set;}

        //ef
        public TeamType? TeamType { get; set; }

        public ApplicationUser? User { get; set; }


    }
}
