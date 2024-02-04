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
        public List<int> RequiredRoles { get; set; }

        public GetProjectsForUserIdQuery(string? userId, List<int> requiredRoles)
        {
            UserId = userId;
            RequiredRoles = requiredRoles;
        }
    }
}
