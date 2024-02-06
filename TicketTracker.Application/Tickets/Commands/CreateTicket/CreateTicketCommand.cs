using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets;

namespace TicketTracker.Application.Tickets.Commands.CreateTicket

{
    public class CreateTicketCommand : TicketCreateDto, IRequest
    {
     
    }
}
