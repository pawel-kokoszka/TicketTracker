using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class ProjectConfigurationDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }

        public int EnvironmentId { get; set; }
    }
}
