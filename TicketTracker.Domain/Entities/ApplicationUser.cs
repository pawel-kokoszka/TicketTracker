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
        public List<Ticket>? Ticket { get; set; }
        public List<Ticket>? EditedTicket { get; set; }
        public List<Ticket>? AssignedTicket { get; set; }

        public List<TeamsUsers>? TeamsUsers { get; set; }




    }
}
