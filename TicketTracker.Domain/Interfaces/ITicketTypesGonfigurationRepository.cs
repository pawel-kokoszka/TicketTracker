using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketTypesGonfigurationRepository
    {
        Task<IEnumerable<TicketTypesGonfiguration>> GetAll();
        Task<IEnumerable<TicketTypesGonfiguration>> GetConfigurationById(int confId);
    }
}