using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;

namespace TicketTracker.Application.HistoryItems.Queries
{
    public class GetHistoryItemsQuery : IRequest<IEnumerable<HistoryItemDto>>
    {
        public int TicketId { get; set; }
    }
}
