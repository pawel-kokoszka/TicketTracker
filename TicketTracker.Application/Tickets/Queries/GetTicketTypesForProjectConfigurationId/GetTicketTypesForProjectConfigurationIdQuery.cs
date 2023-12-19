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
        public GetTicketTypesForProjectConfigurationIdQuery(int projectConfigurationId)
        {
            ProjectConfigurationId = projectConfigurationId;
        }
    }
}
