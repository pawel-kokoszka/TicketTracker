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

    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly TicketTrackerDbContext _dbContext;

        public TicketTypeRepository(TicketTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TicketType>> GetAll()
            => await _dbContext.TicketTypes.ToListAsync();

        public async Task<TicketType> GetTicketTypeById(int typeId)
            => await _dbContext.TicketTypes.FirstAsync(t => t.Id == typeId);


    }
}
