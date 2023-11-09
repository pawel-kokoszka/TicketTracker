using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class TicketDto
    {
        public int TypeId { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }
    }
}
