using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class Environment
    {
        public int Id {get; set;}
        public int EnvironmentTypeId { get; set;}
        public string? Name { get; set;}
        public string? Description { get; set;}

        //EF rel.
        public EnvironmentType? EnvironmentType { get; set;}
        public ProjectConfiguration? ProjectConfiguration { get; set; }
    }
}
