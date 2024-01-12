using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetTicketServices
{
    public class GetTicketServicesForTicketTypeIdQuery : IRequest<IEnumerable<TicketServiceDto>>
    {
        public int TicketTypeConfigurationId { get; set; }

        public GetTicketServicesForTicketTypeIdQuery(int ticketTypeConfigurationId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
         
        }
    }
}
