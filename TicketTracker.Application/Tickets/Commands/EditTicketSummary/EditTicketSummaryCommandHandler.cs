using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicketSummary
{
    internal class EditTicketSummaryCommandHandler : IRequestHandler<EditTicketSummaryCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public EditTicketSummaryCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditTicketSummaryCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                return Unit.Value;//do zastąpienia przy imp. logowania i eh.
            }

            var ticketOryginalData = await _ticketRepository.GetTicketById(request.Id);

            var ticketEditedData = new Ticket();


            //1 dorób mapowanie z TicketEditSummaryDto na ticket z historią 
            //_mapper.Map(request, ticketEditedData);
            // albo jakąś inno metodą 
            //_ticketRepository.MapTicketProperties(ticketEditedData, ticketOryginalData);

            //2 dodaj metodę do zapisywania/mapowania props z historydetails na ticket ----------------------------------------------------


            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
