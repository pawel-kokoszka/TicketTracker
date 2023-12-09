using AutoMapper;
using MediatR;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicket
{
    public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public EditTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Ticket Maker")))
            {
                return Unit.Value;
            }

            var ticket = await _ticketRepository.GetTicketById(request.Id);
            _mapper.Map(request,ticket);

            //_mapper.Map<Domain.Entities.Ticket>(request);            
            //var ticketNew = _mapper.Map<Domain.Entities.Ticket>(request);
            
            ticket.EditedByUserId = currentUser.Id;
            ticket.DateEdited = DateTime.UtcNow;



            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
