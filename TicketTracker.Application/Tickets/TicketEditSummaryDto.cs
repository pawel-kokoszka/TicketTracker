using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets
{
    public class TicketEditSummaryDto : TicketDetailsDto
    {
        public TicketHistoryDto? TicketHistory { get; set; } 
                
    }
}
