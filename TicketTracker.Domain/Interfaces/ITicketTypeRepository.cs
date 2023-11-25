using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketTypeRepository
    {
        Task<IEnumerable<TicketType>> GetAll();
        Task<TicketType> GetTicketTypeById(int typeId);
    }
}
