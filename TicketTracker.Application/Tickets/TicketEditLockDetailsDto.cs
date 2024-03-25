using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketEditLockDetailsDto
    {
        public Guid EditLockId { get; set; }
        public int TicketId { get; set; }
        public DateTime DateEdited { get; set; }
        public bool IsApproved { get; set; }

        public string? UserId { get; set; }
        //public int? TeamID { get; set; }

        public string? SummaryComment { get; set; }

        public bool IsLockNull { get; set; }


    }
}
