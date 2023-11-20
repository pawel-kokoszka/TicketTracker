using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Comments
{
    public class CommentDetailsDto
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        
        public string? UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Message { get; set; }
    }
}
