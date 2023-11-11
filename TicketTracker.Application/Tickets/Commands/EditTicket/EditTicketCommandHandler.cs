using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicket
{
    public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;

        public EditTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetTicketById(request.Id);

            if (ticket == null)
            {
                throw new NullReferenceException("nie pobrałem biletu do wykonania update'u");
            }

            ticket.ShortDescription = request.ShortDescription;
            ticket.Description = request.Description;
            ticket.Id = request.Id;
            ticket.TypeId = request.TypeId;
            ticket.DateCreated = request.DateCreated;
            ticket.PriorityId = request.PriorityId;
            //todo to experiment if this is doable only with auto mapper 

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
