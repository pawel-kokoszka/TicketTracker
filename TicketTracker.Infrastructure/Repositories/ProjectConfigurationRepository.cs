using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.DTOs;
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

        public async Task<IEnumerable<TicketTypeDto>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId)
            => await _dbContext.TicketTypeConfigurations
                        .Include(ttc => ttc.TicketType)
                        //.ThenInclude(ttc => ttc.TicketType)
                        .Where(pc => pc.ProjectConfigurationId == projectConfigurationId)
                        //.SelectMany(s => s.TicketType)
                        .Select(e => new TicketTypeDto
                        {
                            
                            Id = e.TicketType.Id,
                            TypeName = e.TicketType.TypeName,
                            TicketTypeConfigurationId = e.Id
                        })                        
                        .ToListAsync();


        public async Task<IEnumerable<TicketSlaConfiguration>> GetTicketSlasForTicketTypeId(int ticketTypeConfigurationId)
            => await _dbContext.TicketSlaConfigurations
                        
                        .Where(tsc => tsc.TicketTypeConfigurationId == ticketTypeConfigurationId)
                        //.Select(c => new TicketPriority
                        //{
                        //    Id = c.TicketPriorityId,
                        //    Name = c.TicketPriority.Name
                        //})
                        .OrderBy(c => c.PriorityOrderValue)
                        .ToListAsync();

        

        public async Task<IEnumerable<TicketStatus>> GetTicketStatusesForTicketTypeConfigurationId(int ticketTypeConfigurationId, int statusId)
            => await _dbContext.TicketFlowConfigurations
                        .Include(tfc => tfc.TicketStatuses)
                        .Where(tfc => tfc.TicketTypeConfigurationId == ticketTypeConfigurationId && tfc.StatusId == statusId)
                        .Select(ts => new TicketStatus 
                        {
                            Id = ts.NextStatusId,
                            Name = ts.TicketStatuses.Name
                        } )

                        .ToListAsync(); 

        public async Task<TicketFlowConfiguration> GetStatusForNewTicket(int ticketTypeConfigurationId)
            => await _dbContext.TicketFlowConfigurations
                        .Where(tfc => tfc.TicketTypeConfigurationId == ticketTypeConfigurationId && tfc.IsNewTicketFirstStatus == true)
                        .FirstAsync() ;

        //public async Task<IEnumerable<TicketService>> GetTicektServicesForTicketConfigurationId(int ticketTypeConfigurationId)
        //    => await _dbContext.TicketServices
        //            .Join(
        //                _dbContext.TicketServicesConfigurations,
        //                ts => ts.Id,
        //                tsc => tsc.TicketServiceId,
        //                (ts, tsc) => new { TicketService = ts, TicketServiceConfiguration = tsc }
        //            )
        //            .Where(joined => joined.TicketServiceConfiguration.TicketTypeConfigurationId == 1)
        //            .Select(joined => new
        //            {
        //                joined.TicketService.Id,
        //                joined.TicketService.ServiceName
        //            })
        //            .ToListAsync();

        public async Task<IEnumerable<TicketService>> GetTicektServicesForTicketConfigurationId(int ticketTypeConfigurationId)
        {
            var result = await _dbContext.TicketServices
                .Join(
                    _dbContext.TicketServicesConfigurations,
                    ts => ts.Id,
                    tsc => tsc.TicketServiceId,
                    (ts, tsc) => new { TicketService = ts, TicketServiceConfiguration = tsc }
                )
                .Where(joined => joined.TicketServiceConfiguration.TicketTypeConfigurationId == ticketTypeConfigurationId)
                .Select(joined => joined.TicketService)
                .Include(ts => ts.TicketSubServices)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TicketSubService>> GetTicektSubServicesForServiceId(int serviceId)
            => await _dbContext.TicketSubService
                    .Where(tss => tss.TicketServiceId == serviceId)
                    .OrderBy(tss => tss.DisplayOrderValue)
                    .ToListAsync();
    }
}

        //=> await _dbContext.TicketServices
        //            .Include(ts => ts.TicketSubServices)
        //            .Include(ts => ts.TicketServiceConfigurations)
        //            . (ts => ts.TicketServiceConfigurations.)
        //            .ToListAsync();