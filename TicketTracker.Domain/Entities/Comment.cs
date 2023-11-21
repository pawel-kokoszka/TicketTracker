using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    
    public class Comment
    {
        
        public int Id { get; set; }
        
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string? Message { get; set; }

    }
}
