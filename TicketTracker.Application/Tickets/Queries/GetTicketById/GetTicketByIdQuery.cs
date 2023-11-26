using MediatR;

namespace TicketTracker.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketDetailsDto>
    {
        public int TicketId { get; set; }

        public GetTicketByIdQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
