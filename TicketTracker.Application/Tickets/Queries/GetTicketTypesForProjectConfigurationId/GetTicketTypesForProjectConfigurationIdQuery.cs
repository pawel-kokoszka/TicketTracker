using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketTypesForProjectConfigurationId
{
    public class GetTicketTypesForProjectConfigurationIdQuery : IRequest<IEnumerable<TicketTypeDto>>
    {
        public int ProjectConfigurationId { get; set; }
        public string? UserId { get; set; }
        public List<int> RequiredRoles { get; set; }

        public GetTicketTypesForProjectConfigurationIdQuery(int projectConfigurationId, string? userId, List<int> requiredRoles)
        {
            ProjectConfigurationId = projectConfigurationId;
            UserId = userId;
            RequiredRoles = requiredRoles;
        }

        
    }
}
