using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketSla
    {
        public int Id { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public TimeSpan ResolveTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}
