using TicketTracker.Application.DTO.Ticket;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Services
{
    public interface ITicketService
    {
        Task Create(TicketDto ticket);
    }
}