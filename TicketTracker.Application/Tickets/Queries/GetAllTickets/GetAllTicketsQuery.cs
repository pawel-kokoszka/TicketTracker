using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets;

namespace TicketTracker.Application.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>>
    {

    }
}
