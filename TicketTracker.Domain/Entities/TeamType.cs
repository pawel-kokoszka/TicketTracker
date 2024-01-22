using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TeamType
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public bool IsExternal { get; set; }
        public string? Description { get; set; } 
        public int SeniorityLevel { get; set; }

        //ef
        public List<Team>? Teams { get; set; }


    }
}
