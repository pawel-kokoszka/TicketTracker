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
    public class ProjectConfigurationRepository : IProjectConfigurationRepository
    {
        private readonly TicketTrackerDbContext _dbContext;
        public ProjectConfigurationRepository(TicketTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ProjectConfiguration>> GetAllProjectConfigurations()
            => await _dbContext.ProjectConfigurations
                        .Include(pc => pc.Project)
                        .Include(pc => pc.Environment)
                            .ThenInclude(e => e.EnvironmentType)
                        .ToListAsync(); 

        public async Task<ProjectConfiguration> GetProjectConfigurationById(int projConfId)
            => await _dbContext.ProjectConfigurations
                        .Include(pc => pc.Project)
                        .FirstAsync(pc => pc.Id == projConfId);
        
    }
}
