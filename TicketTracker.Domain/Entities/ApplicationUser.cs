using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public UserProfile? UserProfile { get; set; }
        
    }
}
