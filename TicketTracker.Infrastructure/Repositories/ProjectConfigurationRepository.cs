using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using TicketTracker.Application.Tickets;
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

        public async Task<IEnumerable<Project>> GetProjectsForUserId(string userId, List<int> requiredRoles)
        {
           
            //var requiredRoles = new List<int>() { 2, 3 };

            var result = await _dbContext.TeamsUsers
                         .Where(tu => tu.UserId == userId)
                         .Include(tu => tu.Team)
                             .ThenInclude(t => t.TeamRoles)
                                 .ThenInclude(tr => tr.TicketTypeConfiguration.ProjectConfiguration.Project)
                         .SelectMany(r => r.Team.TeamRoles)
                         .Where(e =>  requiredRoles.Contains(e.RoleId))
                         //.Where(e => e.RoleId == createTicektRole || e.RoleId == workOnTicektRole)
                         .Select(e => e.TicketTypeConfiguration.ProjectConfiguration.Project)
                         .Distinct()
                         .ToListAsync();

            return result;
         }

        public async Task<IEnumerable<Domain.Entities.Environment>> GetEnvironmentsForProjectId(int projectId, string userId, List<int> requiredRoles)
        {
            

            var result = await _dbContext.TeamsUsers
                         .Where(tu => tu.UserId == userId)
                         .Include(tu => tu.Team)
                             .ThenInclude(t => t.TeamRoles)

                                 .ThenInclude(tr => tr.TicketTypeConfiguration.ProjectConfiguration.Environment)

                         .SelectMany(r => r.Team.TeamRoles) 
                         .Where(e => requiredRoles.Contains(e.RoleId))                         
                         .Select(e => new Domain.Entities.Environment {
                             Id = e.TicketTypeConfiguration.ProjectConfiguration.EnvironmentId,
                             EnvironmentTypeId = e.TicketTypeConfiguration.ProjectConfiguration.Environment.EnvironmentTypeId,
                             Name = e.TicketTypeConfiguration.ProjectConfiguration.Environment.Name,
                             Description = e.TicketTypeConfiguration.ProjectConfiguration.Environment.Description,
                             ProjectConfiguration = e.TicketTypeConfiguration.ProjectConfiguration                            
                         })
                         .Where(w => w.ProjectConfiguration.ProjectId == projectId)
                         .Distinct()
                         .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TicketTypeDto>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId, string userId, List<int> requiredRoles)
        {
            var result = await (
                from tr in _dbContext.TeamsRoles
                  join t in _dbContext.Teams on tr.TeamId equals t.Id
                    join tu in _dbContext.TeamsUsers on t.Id equals tu.TeamId
                join ttc in _dbContext.TicketTypeConfigurations on tr.TicketTypeConfigurationId equals ttc.Id
                    join tt in _dbContext.TicketTypes on ttc.TicketTypeId equals tt.Id

                where tu.UserId == userId && requiredRoles.Contains(tr.RoleId) && ttc.ProjectConfigurationId == projectConfigurationId
                select(
                    new TicketTypeDto
                    {
                        Id = tt.Id,
                        TypeName = tt.TypeName,
                        TicketTypeConfigurationId = ttc.Id
                    })
                )
                .Distinct()
                .ToListAsync();

            return result;
        }

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


     
        public async Task<TicketSlaConfiguration> GetTicketSlaBySlaId(int slaId)
            => await _dbContext.TicketSlaConfigurations
                        .Where(tsc => tsc.Id == slaId)
                        .FirstAsync();

        public async Task<List<TicketSlaConfiguration>> GetTicketSlaForSlaList(List<int> slaIds)
            => await _dbContext.TicketSlaConfigurations                        
                        .Where(tsc => slaIds.Contains(tsc.Id))
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

        public async Task<List<TicketStatus>> GetTicketStatusesForIdList(List<int> statusIds)
            => await _dbContext.TicketStatuses                        
                        .Where(ts => statusIds.Contains(ts.Id))
                        .ToListAsync();

        public async Task<IEnumerable<TicketStatusDto>> GetTicketStatusesWithPrivilegesForTicketTypeConfigurationId(int ticketTypeConfigurationId, int statusId)
    => await _dbContext.TicketFlowConfigurations
                .Include(tfc => tfc.TicketStatuses)
                .Where(tfc => tfc.TicketTypeConfigurationId == ticketTypeConfigurationId && tfc.StatusId == statusId)
                .Select(ts => new TicketStatusDto
                {
                    StatusId = ts.NextStatusId,
                    Name = ts.TicketStatuses.Name,
                    CreatorCanChangeStatus = ts.CreatorCanChangeStatus,
                    AssignedUserCanChangeStatus = ts.AssignedUserCanChangeStatus
                })

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


        public async Task<List<TicketService>> GetTicketServicesForIdList(List<int> serviceIds)
            => await _dbContext.TicketServices
                        .Where(service => serviceIds.Contains(service.Id))
                        .ToListAsync();

        public async Task<IEnumerable<TicketSubService>> GetTicektSubServicesForServiceId(int serviceId)
            => await _dbContext.TicketSubService
                    .Where(tss => tss.TicketServiceId == serviceId)
                    .OrderBy(tss => tss.DisplayOrderValue)
                    .ToListAsync();

        public async Task<List<TicketSubService>> GetTicketSubServicesForIdList(List<int> subServiceIds)
            => await _dbContext.TicketSubService
                        .Where(subService => subServiceIds.Contains(subService.Id))
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

        public async Task<List<TeamDto>> GetTeamsAndUsersToAssign(int ticketTypeConfigurationId, string? userId)
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

        public async Task<List<TeamDto>> GetTeamsAndUsersToAssign(int ticketTypeConfigurationId, int? teamId)
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
                                                      && ttt.teamsUsers.teams.teamAssignRules.AssigningTeamId == teamId )
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

        public async Task<List<Team>> GetTeamsForIdList(List<int> teamIds)
            => await _dbContext.Teams
                        .Where(team => teamIds.Contains(team.Id))
                        .ToListAsync();

        public async Task<List<ApplicationUser>> GetUsersForIdList(List<string> userIds)
            => await _dbContext.Users
                        .Where(user => userIds.Contains(user.Id))
                        .ToListAsync();
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

        public async Task<List<TeamRoleType>> GetUserRolesRelatedToTicketId(int ticketId, string? userId)
        {
            var roles = await (
                            from t in _dbContext.Tickets
                                join ttc in _dbContext.TicketTypeConfigurations on t.TicketTypeConfigurationId equals ttc.Id
                                    join tr in _dbContext.TeamsRoles on ttc.Id equals tr.TicketTypeConfigurationId
                                        join tu in _dbContext.TeamsUsers on tr.TeamId equals tu.TeamId
                                        join trt in _dbContext.TeamRoleTypes on tr.RoleId equals trt.Id
                            where
                                t.Id == ticketId && tu.UserId == userId
                                
                            select (
                                new TeamRoleType()
                                {
                                    Id = trt.Id,
                                    Name = trt.Name ,
                                    Description = trt.Description
                                })
                            )
                            .Distinct()
                            .ToListAsync() ;

            return roles;
        }

        public async Task<IEnumerable<int>> GetUserTeamsIds(string userId)//todo - move to UserRpository 
            => await _dbContext.TeamsUsers
                        .Where(team => team.UserId == userId)
                        .Select(columns => columns.TeamId)
                        .ToListAsync();
    }
}