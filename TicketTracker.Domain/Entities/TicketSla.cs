using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketSla
    {
        public int Id { get; set; }

        public string? SlaName { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfMinutes { get; set; }
        public bool IsDeadline { get; set; }

        //ef
        public TicketSlaConfiguration? TicketSlaConfiguration { get; set; }

    }
}
