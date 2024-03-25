using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketHistory
    {
        public int Id { get; set; } 
        public Guid EditLockId { get; set; }
        public bool IsApproved { get; set; }

        public int TicketId { get; set; }
        public DateTime DateEdited { get; set; }
        
        public string? UserId { get; set; } 
        //public int TeamId {  get; set; }
        
        public string? SummaryComment { get; set; }

        //ef
        public Ticket? Ticket { get; set; }
        public ApplicationUser? User { get; set; }
        //public Team? Team { get; set; } 
        
        public List<TicketHistoryDetail>? HistoryDetails { get; set; }


    }
}
