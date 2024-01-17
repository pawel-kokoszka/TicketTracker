using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetUserGroup
{
    public class GetCurrentUserIdQuery : IRequest<UserIdDto>
    {
    }
}
