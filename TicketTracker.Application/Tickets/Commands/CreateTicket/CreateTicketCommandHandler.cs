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
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext; 
            _mapper = mapper;
            _projectConfigurationRepository = projectConfigurationRepository;
        }
        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null  || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Ticket Maker")) )
            {
                return Unit.Value;
            }



            var ticket = _mapper.Map<Domain.Entities.Ticket>(request);

            ticket.TicketStatusId = _projectConfigurationRepository.GetStatusForNewTicket(ticket.TicketTypeConfigurationId).Result.StatusId;

            //dodać spr. czy currentuser z tego handlera jest tym samym co przakazny do handlera w command?
            if (ticket.CreatedByUserId != currentUser.Id)
            {
                throw new InvalidOperationException("User sent from client is different from userContext!");
            }

            ticket.CreatedByUserId = currentUser.Id;    
            ticket.DateCreated = DateTime.UtcNow;
            
            ticket.EditedByUserId = currentUser.Id;
            ticket.DateEdited = DateTime.UtcNow;

            await _ticketRepository.Create(ticket);

            return Unit.Value;
        }
    }
}
