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
                        .Include(t => t.TicketSlaConfigurations)
                        .Include(t => t.ProjectConfiguration)
                            .ThenInclude(pc => pc.Project)
                        .Include(t => t.ProjectConfiguration)
                            .ThenInclude(pc => pc.Environment)
                                .ThenInclude(e => e.EnvironmentType)                
                        .Include(t => t.CreatorUser)
                        .Include(t => t.EditorUser)
                        .Include(t => t.AssignedUser)
                        .Include(t => t.TicketStatuses)
                        .Include(ticket => ticket.AssignedTeam)
                        .Include(ticket => ticket.AssigningTeam)
                        .ToListAsync();

        public async Task<Ticket> GetTicketById(int ticketId) 
            => await _dbContext.Tickets
                        .Include(t => t.TicketType)
                        .Include(t => t.TicketSlaConfigurations)                        
                        .Include(t => t.ProjectConfiguration)
                            .ThenInclude(pc => pc.Project)
                        .Include(t => t.ProjectConfiguration)
                            .ThenInclude(pc => pc.Environment)
                                .ThenInclude(e => e.EnvironmentType)
                        .Include(t => t.CreatorUser)
                        .Include(t => t.EditorUser)
                        .Include(t => t.AssignedUser)
                        .Include(t => t.TicketStatuses)
                        .Include(t => t.TicketService)
                        .Include(t => t.TicketSubService)
                        .Include(ticket => ticket.AssignedTeam)
                        .Include(ticket => ticket.AssigningTeam)
                        .FirstAsync(t => t.Id == ticketId);



        //=> await _dbContext.Tickets.FirstAsync(t => t.Id == ticketId);


        // => await _dbContext.Tickets.Include(t => t.Comments).FirstAsync(t => t.Id == ticketId);

        public async Task SaveToDb()
            => await _dbContext.SaveChangesAsync();

        public void MapTicketProperties(Ticket newTicketData, Ticket oldTicketData)
        {
            if (newTicketData is null || oldTicketData is null)
            {
                throw new InvalidOperationException("Update New Ticket Data Operation Failed");
            }
            else
            {
                oldTicketData.AssignedTeamId = newTicketData.AssignedTeamId;
                oldTicketData.AssignedUserId = newTicketData.AssignedUserId;
                oldTicketData.DateEdited = newTicketData.DateEdited;
                oldTicketData.Description = newTicketData.Description;
                oldTicketData.EditedByUserId = newTicketData.EditedByUserId;
                oldTicketData.IsDeleted = newTicketData.IsDeleted;
                oldTicketData.ShortDescription = newTicketData.ShortDescription;
                oldTicketData.TicketServiceId = newTicketData.TicketServiceId;
                oldTicketData.TicketSlaConfigurationId = newTicketData.TicketSlaConfigurationId;
                oldTicketData.TicketStatusId = newTicketData.TicketStatusId;
                oldTicketData.TicketSubServiceId = newTicketData.TicketSubServiceId;
            }
        }

                
    
    
    }
}
