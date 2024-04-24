using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets
{
    public class TicketHistoryDto
    {
        public int Id { get; set; }
        public Guid EditLockId { get; set; }
        public bool IsApproved { get; set; }

        public int TicketId { get; set; }
        public DateTime DateEdited { get; set; }

        public string? UserId { get; set; }
        
        public string? SummaryComment { get; set; }

        public TicketDetailsDto? Ticket { get; set; }
        //public TicketTracker.Domain.Entities.ApplicationUser? User { get; set; }

        public List<TicketHistoryDetailDto>? HistoryDetails { get; set; }
    }
}
