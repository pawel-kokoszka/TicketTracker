using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Infrastructure.DataBaseContext; 

namespace TicketTracker.Infrastructure.Seeders
{
     
    public class TicketTrackerSeeder
    {
        private readonly TicketTrackerDbContext _dbContext;

        public TicketTrackerSeeder(TicketTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Tickets.Any()) 
                {
                    var FirstTicket = new Ticket()
                    {
                        TypeId = 1,
                        EnvId = 1,
                        StatusId = 1,
                        AssignedTo = 1,
                        CreatedByUserId = "1",
                        IsDeleted = false,
                        PriorityId = 1,
                        Description = "Test ticket"
                    };
                    _dbContext.Tickets.Add(FirstTicket);
                    await _dbContext.SaveChangesAsync();    
                }

            }
        }
    }
}
