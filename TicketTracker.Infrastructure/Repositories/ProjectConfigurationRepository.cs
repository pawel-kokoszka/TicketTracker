using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
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

        public async Task<IEnumerable<Project>> GetAllProjects()
            => await _dbContext.Projects                                             
                        .ToListAsync();
        public async Task<IEnumerable<Domain.Entities.Environment>> GetEnvironmentsForProjectId(int projectId)
            => await _dbContext.Environments
                        .Include(e => e.ProjectConfiguration)
                        .Include(e => e.EnvironmentType)
                        .Where(e => e.ProjectConfiguration.ProjectId == projectId)
                        .ToListAsync();

        public async Task<IEnumerable<TicketType>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId)
            => await _dbContext.TicketTypeConfigurations
                        .Include(ttc => ttc.TicketType)
                        //.ThenInclude(ttc => ttc.TicketType)
                        .Where(pc => pc.ProjectConfigurationId == projectConfigurationId)
                        //.SelectMany(s => s.TicketType)
                        .Select(e => new TicketType
                        {
                            //Id = e.TicketType.Id,
                            Id = e.Id,
                            TypeName = e.TicketType.TypeName
                        })                        
                        .ToListAsync();


        public async Task<IEnumerable<TicketSlaConfiguration>> GetTicektSlasForTicketTypeId(int ticketTypeConfigurationId)
            => await _dbContext.TicketSlaConfigurations
                        
                        .Where(tsc => tsc.TicketTypeConfigurationId == ticketTypeConfigurationId)
                        //.Select(c => new TicketPriority
                        //{
                        //    Id = c.TicketPriorityId,
                        //    Name = c.TicketPriority.Name
                        //})
                        .OrderBy(c => c.PriorityOrderValue)
                        .ToListAsync();


    }
}
