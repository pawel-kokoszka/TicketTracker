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
    }
}
