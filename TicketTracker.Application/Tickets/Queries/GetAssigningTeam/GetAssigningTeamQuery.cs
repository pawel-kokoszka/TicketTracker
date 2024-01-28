using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetAssigningTeam
{
    public class GetAssigningTeamQuery : IRequest<int>
    {
        public int TicketTypeConfigurationId { get; set; }
        public string? UserId { get; set; }
     
        public GetAssigningTeamQuery(int ticketTypeConfigurationId, string? userId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
            UserId = userId;
        }
    }
}
