using Microsoft.AspNetCore.Identity;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;
using TicketTracker.Infrastructure.DataBaseContext;

namespace TicketTracker.Infrastructure.Repositories
{
    public class UserRepsitory : IUserRepsitory
    {
        private readonly TicketTrackerDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepsitory(TicketTrackerDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserNameByUserId(string userId)
        {
            var resultUser = await _userManager.FindByIdAsync(userId);

            if (resultUser == null)
            {
                throw new Exception("Provided User ID does not exist.");
            }

            return resultUser;
        }
    }
}
