using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.ApplicationUser
{

    public class CurrentUser
    {
        public string Id {  get; set; }
        public string Email {  get; set; }

        public IEnumerable<string> Roles { get; set; }
        
        public CurrentUser(string id, string email, IEnumerable<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
