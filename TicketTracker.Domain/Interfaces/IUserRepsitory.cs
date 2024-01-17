using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Interfaces
{
    public interface IUserRepsitory
    {
        Task GetCurrentUserId();
    }
}
