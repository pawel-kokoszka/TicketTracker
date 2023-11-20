using Microsoft.EntityFrameworkCore;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;
using TicketTracker.Infrastructure.DataBaseContext;

namespace TicketTracker.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TicketTrackerDbContext _dbContext;

        public CommentRepository(TicketTrackerDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task Create(Comment comment)
        {
            _dbContext.Add(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommmentsByTicketId(int TicketId)
            => await _dbContext.Comments.Where(c => c.TicketId == TicketId).ToListAsync();



    }
}
