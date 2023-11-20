using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Comments.Commands
{
    public class CreateCommentCommand : CreateCommentDto, IRequest
    {
        /// <summary>
        /// Holds Ticket ID number to be set for related comment.
        /// </summary>
        public int TicketId { get; set; } 
    }
}
