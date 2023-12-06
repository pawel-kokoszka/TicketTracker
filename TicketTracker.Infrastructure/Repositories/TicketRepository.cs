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
            => await _dbContext.Tickets
                        .Include(t => t.TicketType)
                        .Include(t => t.TicketPriority)
                        .Include(pc => pc.ProjectConfiguration)
                            .ThenInclude(p => p.Project)
                        .Include(pc => pc.ProjectConfiguration)
                            .ThenInclude(e => e.Environment)
                                .ThenInclude(e => e.EnvironmentType)                
                        .ToListAsync();

        public async Task<Ticket> GetTicketById(int ticketId) 
            => await _dbContext.Tickets
                        .Include(tt => tt.TicketType)
                        .Include(tp => tp.TicketPriority) 
                        .Include(pc => pc.ProjectConfiguration)
                            .ThenInclude(p => p.Project)
                        .Include(pc => pc.ProjectConfiguration)
                            .ThenInclude(e => e.Environment)
                                .ThenInclude(e => e.EnvironmentType)
                        .FirstAsync(t => t.Id == ticketId);

        //=> await _dbContext.Tickets.FirstAsync(t => t.Id == ticketId);


        // => await _dbContext.Tickets.Include(t => t.Comments).FirstAsync(t => t.Id == ticketId);

        public async Task SaveToDb()
            => await _dbContext.SaveChangesAsync();
    }
}
