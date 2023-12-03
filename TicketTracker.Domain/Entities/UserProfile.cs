using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketTracker.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public string? Description { get; set;}
        public bool IsActive { get; set; }

        //ef
        public UserPreference? UserPreference {  get; set; }    
                
        //public IdentityUser? IdentityUser { get; set; }
    }
}
