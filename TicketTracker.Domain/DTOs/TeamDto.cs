using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.DTOs
{
    public class TeamDto
    {
        public int TeamId { get; set; }

        public string? TeamName { get; set; }
        public string? UserId { get; set; }

        public string? UserEmail { get; set; }
        public int UserSeniorityOrder { get; set; }
    }
}
