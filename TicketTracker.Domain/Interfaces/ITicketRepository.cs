using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);

        Task CreateHistoryEntry(TicketHistory historyEntry);
        Task CreateHistoryDetails(List<TicketHistoryDetail> historyDetails);
        //Task GetHistoryDetailsForEditLockId(List<TicketHistoryDetail> historyDetails);

        Task<TicketHistory> GetTicketLockByTicketId(int ticketId);
        Task<TicketHistory> GetTicketHistoryEntryByLockIdAndTicketId(int ticketHistoryId);

        Task<List<TicketHistoryDetail>> GetUnsavedTicketProperties(int ticketHistoryId);

        Task<IEnumerable<Ticket>> GetAll();

        Task<Ticket> GetTicketById(int ticketId);
        /// <summary>
        /// wrap for dbContext.SaveChanges
        /// in JK lesson it was named Commit()
        /// </summary>
        /// <returns></returns>
        Task SaveToDb();
        Task UpdateHistoryEntry(TicketHistory historyEntry);

        void MapTicketProperties(Ticket newTicketData, Ticket oldTicketData);

        Task<IEnumerable<TicketHistory>>GetHistoryEventsByTicketId(int ticketId);
        Task SaveChangesToAlredyEditedHistoryDetails(IEnumerable<TicketHistoryDetail> DiscardedPropertyChangesInEditSession);
    }
}
