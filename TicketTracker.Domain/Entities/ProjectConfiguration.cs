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

        //EF relations:
        //public Ticket? Ticket { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public Project? Project { get; set; }  
        public Environment? Environment { get; set; }
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }

    }
}
