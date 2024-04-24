using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketHistoryDetailDto
    {
        public int Id { get; set; }
        public int TicketHistoryId { get; set; }
        public string? TicketPropertyName { get; set; }
        public string? TicketPropertyDisplayName { get; set; }

        public string? PropertyNewValue { get; set; }
        public string? PropertyNewDisplayValue { get; set; }

        public string? PropertyOldValue { get; set; }
        public string? PropertyOldDisplayValue { get; set; }

        public string? Comment { get; set; }
    }
}
