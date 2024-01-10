using MediatR;

namespace TicketTracker.Application.Tickets.Queries.GetTicketStatusesForTicketTypeConfiguration
{
    public class GetTicketStatusesForTicketTypeConfigurationQuery : IRequest<IEnumerable<TicketStatusDto>>
    {
        public int TicketTypeConfigurationId { get; set; }
        public int StatusId { get; set; }

        public GetTicketStatusesForTicketTypeConfigurationQuery(int ticketTypeConfigurationId, int statusId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
            StatusId = statusId;
        }
    }
}
