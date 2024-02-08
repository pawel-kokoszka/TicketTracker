using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetUserRolesRelatedToTicketId
{
    public class GetUserRolesRelatedToTicketIdQuery : IRequest<UserRolesDto>
    {
        public GetUserRolesRelatedToTicketIdQuery(int ticketId, string? userId)
        {
            TicketId = ticketId;
            UserId = userId;
        }

        public int TicketId { get; set; }
        public string? UserId { get; set; }
    }
}
