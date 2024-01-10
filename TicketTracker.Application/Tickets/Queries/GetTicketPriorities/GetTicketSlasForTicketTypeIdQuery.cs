using MediatR;

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
