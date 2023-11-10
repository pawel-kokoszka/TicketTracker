using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketDetailsDto>
    {
        public int TicketId { get; set; }

        public GetTicketByIdQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
