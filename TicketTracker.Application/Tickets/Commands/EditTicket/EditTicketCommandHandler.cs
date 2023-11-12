using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicket
{
    public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public EditTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetTicketById(request.Id);

            //_mapper.Map<Domain.Entities.Ticket>(request);
            _mapper.Map(request,ticket);

            //ticket.ShortDescription = request.ShortDescription;
            //ticket.Description = request.Description;
            //ticket.Id = request.Id;
            //ticket.TypeId = request.TypeId;
            //ticket.DateCreated = request.DateCreated;
            //ticket.PriorityId = request.PriorityId;
            //todo to experiment if this is doable only with auto mapper 

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
