using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRpository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRpository = ticketRepository;
        }
        public async Task Create(Ticket ticket)
        {
            await _ticketRpository.Create(ticket);
        }
    }
}
