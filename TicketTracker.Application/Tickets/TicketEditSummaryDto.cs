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

        public bool IsTicketSlaChanged { get; set; }
        public bool IsDescriptionChanged { get; set; }
        public bool IsShortDescriptionChanged { get; set; }
        public bool IsTicketStatusChanged { get; set; }
        public bool IsTicketServiceCategoryChanged { get; set; }
        public bool IsTicketSubServiceChanged { get; set; }
        public bool IsAssignedTeamChanged { get; set; }
        public bool IsAssignedUserChanged { get; set; }



    }
}
