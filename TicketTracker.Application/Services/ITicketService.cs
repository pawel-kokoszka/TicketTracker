using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Services
{
    public interface ITicketService
    {
        Task Create(Ticket ticket);
    }
}