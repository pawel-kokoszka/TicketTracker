using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketServicesConfigurationsRepository
    {
        Task<IEnumerable<TicketServicesConfiguration>> GetAll();
        Task<IEnumerable<TicketServicesConfiguration>> GetConfigurationById(int confId);
    }
}
