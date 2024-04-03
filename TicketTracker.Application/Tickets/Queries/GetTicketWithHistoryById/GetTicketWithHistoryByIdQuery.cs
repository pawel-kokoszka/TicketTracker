using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketWithHistoryById
{
    public class GetTicketWithHistoryByIdQuery : IRequest< TicketEditSummaryDto>
    {
        public int TicketId { get; set; }

        public GetTicketWithHistoryByIdQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
