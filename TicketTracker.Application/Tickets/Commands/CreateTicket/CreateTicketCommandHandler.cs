using AutoMapper;
using MediatR;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;


        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
        {
            _userContext = userContext; 
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null  || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Ticket Maker")) )
            {
                return Unit.Value;
            }

            var ticket = _mapper.Map<Domain.Entities.Ticket>(request);

            ticket.CreatedByUserId = currentUser.Id;    
            ticket.CreatedByUserName = currentUser.Email;    

            await _ticketRepository.Create(ticket);

            return Unit.Value;
        }
    }
}
