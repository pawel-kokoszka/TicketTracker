using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.DTO.Ticket
{
    public class TicketDto
    {
        public int TypeId { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string? Description { get; set; }
    }
}
