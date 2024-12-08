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
        public Guid EditLockId { get; set; }
        public bool IsDiscarded { get; set; }
        public string? TicketPropertyName { get; set; }
        public string? PropertyNewValue { get; set; }
        //public string? PropertyNewDisplayValue { get; set; }

        public string? PropertyOldValue { get; set; }
        //public string? PropertyOldDisplayValue { get; set; }

        public string? Comment { get; set; }
        //trzeba dodać ticketlockid 

        //ef
        public TicketHistory? TicketHistory { get; set; }
    }
}
