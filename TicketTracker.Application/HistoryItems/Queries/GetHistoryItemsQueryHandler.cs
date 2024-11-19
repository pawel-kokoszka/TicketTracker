using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.HistoryItems.Queries
{
    internal class GetHistoryItemsQueryHandler : IRequestHandler<GetHistoryItemsQuery, IEnumerable<HistoryItemDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;

        public GetHistoryItemsQueryHandler(ICommentRepository commentRepository, ITicketRepository ticketRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HistoryItemDto>> Handle(GetHistoryItemsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommmentsByTicketId(request.TicketId);
            var historyEvents = await _ticketRepository.GetHistoryByTicketId(request.TicketId);
            //mapowanie z comments na historyItem 
            //dodać comments do items 
            var items = _mapper.Map<IEnumerable<HistoryItemDto>>(comments);

            //i oznaczyć je jakoś jako comments 
            foreach (var item in items)
            {
                item.ItemType = HistoryItem.Comment;
            }

            var historyItems = new List<HistoryItemDto>();

            foreach (var historyEvent in historyEvents)
            {
                var message = "";
                    
                foreach (var detail in historyEvent.HistoryDetails!)
                {
                    message += $"Propery: {detail.TicketPropertyName} changed to {detail.PropertyNewValue} from {detail.PropertyOldValue}.\n";
                }

                var historyItem = _mapper.Map<HistoryItemDto>(historyEvent);
                historyItem.Message = message;
                historyItem.ItemType = HistoryItem.HistoryEvent;

                historyItems.Add(historyItem);
            }

            historyItems.AddRange(items);
            //var items2 = _mapper.Map<IEnumerable<HistoryItemDto>>(historyEvents);


            //z ticketRepo pobrać historię
            //dodać elementy history do items 
            //i oznaczyć je jako hist event

            //var items = new List<HistoryItemDto>();

            return historyItems; 
        }
    }
}
