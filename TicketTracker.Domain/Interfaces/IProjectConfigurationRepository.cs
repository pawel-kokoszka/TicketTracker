using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface IProjectConfigurationRepository
    {
        Task<IEnumerable<ProjectConfiguration>> GetAllProjectConfigurations();
        Task<ProjectConfiguration> GetProjectConfigurationById(int projConfId);

        Task<IEnumerable<Project>> GetAllProjects();
        Task<IEnumerable<Entities.Environment>> GetEnvironmentsForProjectId(int projectId);
        //Task<IEnumerable<TicketType>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId);
        Task<IEnumerable<TicketType>> GetTicektTypesForProjectConfigurationId(int projectConfigurationId);

        Task<IEnumerable<TicketPriority>> GetTicektPrioritiesForTicketTypeId(int ticketTypeConfigurationId);
    }
}
