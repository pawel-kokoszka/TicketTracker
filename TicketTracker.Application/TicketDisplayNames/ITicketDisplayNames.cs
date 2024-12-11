using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets;

namespace TicketTracker.Application.TicketDisplayNames
{
    public interface ITicketDisplayNames
    {
        void AddDisplayNamesToHistoryDetails(List<TicketHistoryDetailDto> historyPropertyDetails);
    }
}
