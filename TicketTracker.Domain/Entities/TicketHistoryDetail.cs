using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketHistoryDetail
    {
        public int Id { get; set; }
        public int TicketHistoryId { get; set; }
        public string? TicketPropertyName { get; set; }
        public string? PropertyNewValue { get; set; }
        public string? PropertyOldValue { get; set; }
        public string? Comment { get; set; }


        //ef
        public TicketHistory? TicketHistory { get; set; }
    }
}
