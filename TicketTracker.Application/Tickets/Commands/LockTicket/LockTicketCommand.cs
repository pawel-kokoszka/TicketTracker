using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Commands.LockTicket
{
    public class LockTicketCommand : TicketLockDto, IRequest 
    {
        //public int TicketId { get; set; }

        public LockTicketCommand(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
