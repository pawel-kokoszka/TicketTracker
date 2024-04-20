using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Tickets.Commands.UnlockTicket
{
    public class UnlockTicketCommand : TicketLockDto, IRequest
    {
        public UnlockTicketCommand(int ticketId)
        {
            TicketId = ticketId;
        }

    }
}
