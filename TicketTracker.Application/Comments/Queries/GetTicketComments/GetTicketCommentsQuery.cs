using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Comments.Queries.GetTicketComments
{
    public class GetTicketCommentsQuery : IRequest<IEnumerable<CommentDetailsDto>>
    {
        public int TicketId { get; set; }
    }
}
