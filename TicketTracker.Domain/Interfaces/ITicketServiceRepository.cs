using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketServiceRepository
    {
        Task<IEnumerable<TicketService>> GetAll();
        Task<TicketService> GetTicketServiceById(int serviceId);
    }
}
