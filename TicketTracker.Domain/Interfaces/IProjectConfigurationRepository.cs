﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.DTOs;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        Task<IEnumerable<ProjectConfiguration>> GetAllProjectConfigurations();
        Task<ProjectConfiguration> GetProjectConfigurationById(int projConfId);

        Task<IEnumerable<Project>> GetAllProjects();
        Task<IEnumerable<Project>> GetProjectsForUserId(string UserId);

        Task<IEnumerable<Entities.Environment>> GetEnvironmentsForProjectId(int projectId);
        //Task<IEnumerable<TicketType>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId);
        Task<IEnumerable<Domain.DTOs.TicketTypeDto>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId);

        Task<IEnumerable<TicketSlaConfiguration>> GetTicketSlasForTicketTypeId(int ticketTypeConfigurationId);
        
        Task<IEnumerable<TicketStatus>> GetTicketStatusesForTicketTypeConfigurationId(int ticketTypeConfigurationId, int statusId);
        Task<TicketFlowConfiguration> GetStatusForNewTicket(int ticketTypeConfigurationId);
        Task<IEnumerable<TicketService>> GetTicektServicesForTicketConfigurationId(int ticketTypeConfigurationId);

        Task<IEnumerable<TicketSubService>> GetTicektSubServicesForServiceId(int serviceId);

        Task<int> GetAssigningTeam(int ticketTypeConfigurationId, string? userId);
        Task<IEnumerable<TeamDto>> GetTeamsToAssign(int ticketTypeConfigurationId, string? userId);
    }
}
