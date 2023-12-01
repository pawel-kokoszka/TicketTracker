using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketStatesGonfigurationsRepository
    {
        Task<IEnumerable<TicketStatesGonfiguration>> GetAll();
        Task<IEnumerable<TicketStatesGonfiguration>> GetGonfigurationById(int confId);
    }
}
