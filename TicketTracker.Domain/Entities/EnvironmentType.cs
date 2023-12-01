using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class EnvironmentType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }


        //EF rel.
        public Environment? Environment { get; set; }
        

    }
}
