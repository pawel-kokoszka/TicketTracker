﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.DTOs;
using TicketTracker.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TicketTracker.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        Task<IEnumerable<ProjectConfiguration>> GetAllProjectConfigurations();
        Task<ProjectConfiguration> GetProjectConfigurationById(int projConfId);

        Task<IEnumerable<Project>> GetAllProjects();
        Task<IEnumerable<Project>> GetProjectsForUserId(string UserId, List<int> requiredRoles);

        Task<IEnumerable<Entities.Environment>> GetEnvironmentsForProjectId(int projectId, string userId, List<int> requiredRoles);
        
        Task<IEnumerable<Domain.DTOs.TicketTypeDto>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId, string userId, List<int> requiredRoles);

        Task<IEnumerable<TicketSlaConfiguration>> GetTicketSlasForTicketTypeId(int ticketTypeConfigurationId);
        Task<TicketSlaConfiguration> GetTicketSlaBySlaId(int slaId);

        Task<IEnumerable<TicketStatus>> GetTicketStatusesForTicketTypeConfigurationId(int ticketTypeConfigurationId, int statusId);
        Task<IEnumerable<TicketStatusDto>> GetTicketStatusesWithPrivilegesForTicketTypeConfigurationId(int ticketTypeConfigurationId, int statusId);

        Task<TicketFlowConfiguration> GetStatusForNewTicket(int ticketTypeConfigurationId);
        Task<IEnumerable<TicketService>> GetTicektServicesForTicketConfigurationId(int ticketTypeConfigurationId);

        Task<IEnumerable<TicketSubService>> GetTicektSubServicesForServiceId(int serviceId);

        Task<int> GetAssigningTeam(int ticketTypeConfigurationId, string? userId);
        Task<List<TeamDto>> GetTeamsAndUsersToAssign(int ticketTypeConfigurationId, string? userId);
        Task<List<TeamDto>> GetTeamsAndUsersToAssign(int ticketTypeConfigurationId, int? teamId);

        Task<List<TeamRoleType>> GetUserRolesRelatedToTicketId(int ticketId, string? userId);

        Task<IEnumerable<int>> GetUserTeamsIds(string userId);
    }
}
