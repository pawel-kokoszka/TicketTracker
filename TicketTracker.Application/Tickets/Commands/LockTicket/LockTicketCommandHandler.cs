using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Entities;
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
                throw new InvalidOperationException($"Locking Ticket failure, Ticket {request.TicketId} is already locked");
            }

            var lockID = Guid.NewGuid();

            ticketToLock.EditLockId = lockID;

            //dodaj tworzenie rekordów history i details 

            var historyEntry = new TicketHistory();

            historyEntry.HistoryDetails = new List<TicketHistoryDetail>();

            historyEntry.EditLockId = lockID; 

            historyEntry.TicketId = request.TicketId;

            historyEntry.DateEdited = DateTime.UtcNow;

            historyEntry.UserId = currentUser.Id;
            
                //historyEntry.TeamId = 1; //to delete from dto and db 

            //historyEntry.HistoryDetails.Add(new TicketHistoryDetail());//trzeba dodać warunek, jesli nie zmienionych żadnych prop to niech nie tworzy Listy 

            await _ticketRepository.CreateHistoryEntry(historyEntry);

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }
    }
}
