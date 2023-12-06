using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface IEnvironmentRepository
    {
        Task<IEnumerable<Entities.Environment>> GetAll();
        Task<Entities.Environment> GetEnvironmentById(int environmentId);
    }
}
