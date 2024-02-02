using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetProjectsForUserId
{
    public class GetProjectsForUserIdQuery : IRequest<IEnumerable<ProjectDto>>
    {
        public string? UserId { get; set; }

        public GetProjectsForUserIdQuery(string? userId)
        {
            UserId = userId;
        }
    }
}
