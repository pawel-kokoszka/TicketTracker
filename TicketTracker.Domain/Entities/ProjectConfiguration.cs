using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class ProjectConfiguration
    {
        public int Id { get; set; } 
        public string? Description { get; set; }
        public int ProjectId {  get; set; }
        public int EnvironmentId { get; set; }

    }
}
