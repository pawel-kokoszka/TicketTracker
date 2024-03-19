using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.LockTicket
{
    public class LockTicketCommandHandler : IRequestHandler<LockTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;

        public LockTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(LockTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                return Unit.Value;
            }

            var ticketToLock = await _ticketRepository.GetTicketById(request.TicketId);

            if (ticketToLock.EditLockId != null)
            {
                throw new InvalidOperationException("Locking Ticket failure, Ticket is already locked");
            }

            var lockID = Guid.NewGuid();

            ticketToLock.EditLockId = lockID;

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
