using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTeamsToAssign
{
    public class GetTeamsToAssignQuery : IRequest<IEnumerable<TeamDto>>
    {
        public int TicketTypeConfigurationId {  get; set; }
        public string UserId { get; set; }
        public GetTeamsToAssignQuery(int ticketTypeConfigurationId, string userId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
            UserId = userId;
        }



    }
}
