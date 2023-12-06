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
    public class TicketPriorityRepository : ITicketPriorityRepository
    {
        private readonly TicketTrackerDbContext _dbContext;

        public TicketPriorityRepository(TicketTrackerDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
        public async Task<IEnumerable<TicketPriority>> GetAll()
            => await _dbContext.TicketPriorities.ToListAsync();

        public async Task<TicketPriority> GetPriorityById(int priorityId)
            => await _dbContext.TicketPriorities.FirstAsync(p => p.Id == priorityId);
    }
}
