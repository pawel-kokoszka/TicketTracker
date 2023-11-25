using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketPriorityRepository
    {
        Task<IEnumerable<TicketPriority>> GetAll();
        Task<TicketPriority> GetPriorityById(int priorityId);
    }
}
