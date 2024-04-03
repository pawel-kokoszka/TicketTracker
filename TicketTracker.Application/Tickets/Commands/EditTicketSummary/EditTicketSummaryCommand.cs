using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Commands.EditTicketSummary
{
    public class EditTicketSummaryCommand: TicketEditSummaryDto, IRequest
    {
    }
}
