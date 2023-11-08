using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;
using TicketTracker.Infrastructure.DataBaseContext;

namespace TicketTracker.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketTrackerDbContext _dbContext;
        public TicketRepository(TicketTrackerDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task Create(Ticket ticket)
        {
            _dbContext.Add(ticket);
            await _dbContext.SaveChangesAsync();  
        }

        public async Task<IEnumerable<Ticket>> GetAll()
            => await _dbContext.Tickets.ToListAsync();
    }
}
