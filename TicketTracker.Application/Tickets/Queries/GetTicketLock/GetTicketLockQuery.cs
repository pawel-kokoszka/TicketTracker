using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketLock
{
    public class GetTicketLockQuery : IRequest<TicketEditLockDetailsDto>
    {
        public int TicketId { get; set; }

        public GetTicketLockQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }

}