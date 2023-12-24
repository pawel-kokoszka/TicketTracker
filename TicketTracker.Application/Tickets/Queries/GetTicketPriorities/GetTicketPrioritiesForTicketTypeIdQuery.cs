using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketPriorities
{
    public class GetTicketPrioritiesForTicketTypeIdQuery : IRequest<IEnumerable<TicketPriorityDto>>
    {
        public int TicketTypeConfigurationId { get; set; }

        public GetTicketPrioritiesForTicketTypeIdQuery(int ticketTypeConfigurationId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
        }
    }
}
