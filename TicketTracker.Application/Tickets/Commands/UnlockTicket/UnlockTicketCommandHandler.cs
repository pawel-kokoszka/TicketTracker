using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Application.Tickets.Commands.LockTicket;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.UnlockTicket
{
    internal class UnlockTicketCommandHandler : IRequestHandler<UnlockTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        public UnlockTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(UnlockTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                return Unit.Value;
            }

            var ticketToUnlock = await _ticketRepository.GetTicketById(request.TicketId);

            if (ticketToUnlock.EditLockId == null)
            {
                throw new InvalidOperationException($"Unlocking Ticket operation failure, Ticket {request.TicketId} is already unlocked");
            }

            ticketToUnlock.EditLockId = null;

            ticketToUnlock.DateEdited = DateTime.UtcNow;           
            ticketToUnlock.EditedByUserId = currentUser.Id;



            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
