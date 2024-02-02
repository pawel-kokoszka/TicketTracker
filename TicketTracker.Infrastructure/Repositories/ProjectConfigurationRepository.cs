﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<IEnumerable<Project>> GetProjectsForUserId(string UserId)
        {
            int roleID2 = 2;
            int roleID3 = 3;

            var result = await _dbContext.TeamsUsers
                         .Where(tu => tu.UserId == UserId)
                         .Include(tu => tu.Team)
                             .ThenInclude(t => t.TeamRoles.Where(tr => tr.RoleId == roleID2 || tr.RoleId == roleID3))
                                 //.Where(tr => tr.Team.TeamRoles.Contains(=> ttt. ) )

                                 .ThenInclude(tr => tr.TicketTypeConfiguration.ProjectConfiguration.Project)

                         .SelectMany(r => r.Team.TeamRoles) //.Select(e => e.TicketTypeConfiguration.ProjectConfiguration.Project))
                         .Where(e => e.RoleId == roleID2)
                         .Select(e => e.TicketTypeConfiguration.ProjectConfiguration.Project)
                         .Distinct()
                         .ToListAsync();


            
            //var projects = new List<Project>();

            return result;
         }
        //public async Task<IEnumerable<Project>> GetProjectsForUserId(string UserId)
        //{
        //    int roleID2 = 2;
        //    int roleID3 = 3;

        //   var result = await _dbContext.TeamsUsers
        //                .Where(tu => tu.UserId == UserId)
        //                .Include(tu => tu.Team)
        //                    .ThenInclude(t => t.TeamRoles.Where(tr => tr.RoleId == roleID2 || tr.RoleId == roleID3))
        //                    //.Where(tr => tr.Team.TeamRoles.Contains(=> ttt. ) )

        //                        .ThenInclude(tr => tr.TicketTypeConfiguration.ProjectConfiguration.Project)
                                    
        //                .SelectMany(r => r.Team.TeamRoles.Select(e => e.TicketTypeConfiguration.ProjectConfiguration.Project))

        //                .ToListAsync();


            
        //    var projects = new List<Project>();

        //    return result;
        // }

        //public async Task<IEnumerable<Project>> GetProjectsForUserId3(string UserId)
        //    => await _dbContext.TeamsUsers
        //                .Where(tu => tu.UserId == UserId)
        //                .Include(tu => tu.Team)
        //                    .ThenInclude(t => t.TeamRoles)
                                
        //                        .ThenInclude(tr => tr.TicketTypeConfiguration)
        //                            .ThenInclude(ttc => ttc.ProjectConfiguration)
        //                                .ThenInclude(pc => pc.Project)
        //                .Select(p => 
        //                    new Project { 
        //                        Id = p.Team.TeamRoles
        //                    }
        //                )

        //                .ToListAsync();

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

        public async Task<IEnumerable<TeamDto>> GetTeamsToAssign3(int ticketTypeConfigurationId, int assigningTeamId)
        {
             //assigningTeamId = 1; //await GetAssigningTeam(ticketTypeConfigurationId, userId);

            var teamsToAssign = new List<TeamDto>();

            teamsToAssign = await _dbContext.TicketTypeTeamAssignRules
                                        .Join(
                                            _dbContext.Teams,
                                            teamAssignRules => teamAssignRules.AssignedTeamId,
                                            teams => teams.Id,
                                            (teamAssignRules, teams) => new { teamAssignRules = teamAssignRules, teams = teams })
                                        .Join(
                                            _dbContext.TeamsUsers,
                                            teams => teams.teams.Id,
                                            teamsUsers => teamsUsers.TeamId,
                                            (teams, teamsUsers) => new { teams = teams, teamsUsers = teamsUsers })
                                        .Join(
                                            _dbContext.Users,
                                            teamsUsers => teamsUsers.teamsUsers.UserId,
                                            users => users.Id,
                                            (teamsUsers, users) => new { teamsUsers = teamsUsers, users = users })
                                        .Where(ttt => ttt.teamsUsers.teams.teamAssignRules.TicketTypeConfigurationId == ticketTypeConfigurationId 
                                            && ttt.teamsUsers.teams.teamAssignRules.AssigningTeamId == assigningTeamId)
                                        .Select(columns => new TeamDto
                                        {
                                            TeamId = columns.teamsUsers.teams.teamAssignRules.AssignedTeamId,
                                            TeamName = columns.teamsUsers.teams.teams.Name,
                                            UserId = columns.users.Id,
                                            UserEmail = columns.users.Email,
                                            UserSeniorityOrder = columns.teamsUsers.teamsUsers.SeniorityOrder
                                        })
                                        .ToListAsync();
            
            return teamsToAssign;
        }

        public async Task<int> GetAssigningTeam(int ticketTypeConfigurationId, string? userId)
        {
            var assigningTeamId = await _dbContext.TicketTypeTeamAssignRules
                                        .Join(
                                            _dbContext.Teams,
                                            teamAssignRules => teamAssignRules.AssigningTeamId,
                                            teams => teams.Id,
                                            (teamAssignRules, teams) => new {TicketTypeTeamAssigningRule = teamAssignRules, Team = teams }
                                         )
                                        .Join(
                                            _dbContext.TeamsUsers,
                                            teamAssignRules_teams => teamAssignRules_teams.Team.Id,
                                            teamsUsers => teamsUsers.TeamId,
                                            (teamAssignRules_teams, teamsUsers) => new { join1 = teamAssignRules_teams, join2 = teamsUsers } 
                                         )
                                        .Where
                                         (
                                            teamAssignRules_teams_teamsUsers => teamAssignRules_teams_teamsUsers.join1.TicketTypeTeamAssigningRule.TicketTypeConfigurationId == ticketTypeConfigurationId
                                            && teamAssignRules_teams_teamsUsers.join2.UserId == userId  
                                         )
                                        .Select(teamAssignRules => teamAssignRules.join1.TicketTypeTeamAssigningRule.AssigningTeamId)
                                        .Distinct()
                                        .FirstAsync();
         
            return assigningTeamId;
        }

        public async Task<IEnumerable<TeamDto>> GetTeamsToAssign(int ticketTypeConfigurationId, string? userId)
        {
            //assigningTeamId = 1; //await GetAssigningTeam(ticketTypeConfigurationId, userId);

            var teamsToAssign = new List<TeamDto>();
            //var assigningTeamId = /*await*/ 

            teamsToAssign = await _dbContext.TicketTypeTeamAssignRules
                                        .Join(
                                            _dbContext.Teams,
                                            teamAssignRules => teamAssignRules.AssignedTeamId,
                                            teams => teams.Id,
                                            (teamAssignRules, teams) => new { teamAssignRules = teamAssignRules, teams = teams })
                                        .Join(
                                            _dbContext.TeamsUsers,
                                            teams => teams.teams.Id,
                                            teamsUsers => teamsUsers.TeamId,
                                            (teams, teamsUsers) => new { teams = teams, teamsUsers = teamsUsers })
                                        .Join(
                                            _dbContext.Users,
                                            teamsUsers => teamsUsers.teamsUsers.UserId,
                                            users => users.Id,
                                            (teamsUsers, users) => new { teamsUsers = teamsUsers, users = users })
                                        .Where(ttt => ttt.teamsUsers.teams.teamAssignRules.TicketTypeConfigurationId == ticketTypeConfigurationId
                                            && ttt.teamsUsers.teams.teamAssignRules.AssigningTeamId ==  
                                                
                                                    _dbContext.TicketTypeTeamAssignRules
                                                    .Join(
                                                        _dbContext.Teams,
                                                        teamAssignRules => teamAssignRules.AssigningTeamId,
                                                        teams => teams.Id,
                                                        (teamAssignRules, teams) => new { TicketTypeTeamAssigningRule = teamAssignRules, Team = teams }
                                                     )
                                                    .Join(
                                                        _dbContext.TeamsUsers,
                                                        teamAssignRules_teams => teamAssignRules_teams.Team.Id,
                                                        teamsUsers => teamsUsers.TeamId,
                                                        (teamAssignRules_teams, teamsUsers) => new { join1 = teamAssignRules_teams, join2 = teamsUsers }
                                                     )
                                                    .Where
                                                     (
                                                        teamAssignRules_teams_teamsUsers => teamAssignRules_teams_teamsUsers.join1.TicketTypeTeamAssigningRule.TicketTypeConfigurationId == ticketTypeConfigurationId
                                                        && teamAssignRules_teams_teamsUsers.join2.UserId == userId
                                                     )
                                                    .Select(teamAssignRules => teamAssignRules.join1.TicketTypeTeamAssigningRule.AssigningTeamId)
                                                    .Distinct()
                                                    .First()
                                                
                                            )
                                        .Select(columns => new TeamDto
                                        {
                                            TeamId = columns.teamsUsers.teams.teamAssignRules.AssignedTeamId,
                                            TeamName = columns.teamsUsers.teams.teams.Name,
                                            UserId = columns.users.Id,
                                            UserEmail = columns.users.Email,
                                            UserSeniorityOrder = columns.teamsUsers.teamsUsers.SeniorityOrder
                                        })
                                        .ToListAsync();

            return teamsToAssign;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<Ticket>> GetTeamsToAssign4(int ticketTypeConfigurationId, string? userId)
            {
                //assigningTeamId = 1; //await GetAssigningTeam(ticketTypeConfigurationId, userId);

                var teamsToAssign =  await _dbContext.Tickets
                                            
                                            .Where
                                            (   t => t.IsDeleted == false 
                                                &&
                                                 _dbContext.TicketTypes
                                                                .Where(tt => tt.Id == 1)
                                                                .Select(tt => tt.Id)
                                                                .Contains(t.TypeId)        
                                            )
                                            .ToListAsync();

                return teamsToAssign;
            }
    }
}