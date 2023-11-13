using AutoMapper;
using MediatR;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;


        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, IUserContext userContext)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _userContext = userContext; 
        }
        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Domain.Entities.Ticket>(request);

            ticket.CreatedByUserId = _userContext.GetCurrentUser().Id;    
            ticket.CreatedByUserName = _userContext.GetCurrentUser().Email;    

            await _ticketRepository.Create(ticket);

            return Unit.Value;
        }
    }
}
