using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketPriorities
{
    public class GetTicketSlasForTicketTypeIdQuery : IRequest<IEnumerable<TicketSlaDto>>
    {
        public int TicketTypeConfigurationId { get; set; }

        public GetTicketSlasForTicketTypeIdQuery(int ticketTypeConfigurationId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
        }
    }
}
