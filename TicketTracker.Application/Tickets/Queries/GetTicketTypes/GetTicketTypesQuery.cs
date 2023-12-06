using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets.Queries.GetTicketTypes
{
    public class GetTicketTypesQuery : IRequest<IEnumerable<TicketTypeDto>>
    {
    }
}
