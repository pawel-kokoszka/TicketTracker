using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.ApplicationUser
{
    public class UserContext : IUserContext 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public UserContext(IHttpContextAccessor httpContextAccessor, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectConfigurationRepository = projectConfigurationRepository;

        }

        public CurrentUser GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present!");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser(id, email, roles);
        }

        public async Task<IEnumerable<int>> GetUserTeamsIds(string userId)
        {
            var teamsResult = await _projectConfigurationRepository.GetUserTeamsIds( userId); 

            return teamsResult;
        }
    }
}
