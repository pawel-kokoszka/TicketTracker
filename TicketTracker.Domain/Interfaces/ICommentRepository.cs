using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task Create(Comment comment);
        Task<IEnumerable<Comment>> GetCommmentsByTicketId(int TicketId);

    }
}
