using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;
using TicketTracker.Domain.Entities;
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
            var historyItemsCombined = new List<HistoryItemDto>();

            var comments = await _commentRepository.GetCommmentsByTicketId(request.TicketId);
            var historyEvents = await _ticketRepository.GetHistoryEventsByTicketId(request.TicketId);


            var historyItemComments = _mapper.Map<IEnumerable<HistoryItemDto>>(comments);
            AddHistoryItemType(historyItemComments, HistoryItem.Comment);

            var historyItemEvents = MapHistoryEvents(historyEvents);

            historyItemsCombined.AddRange(historyItemEvents);
            historyItemsCombined.AddRange(historyItemComments);

            var sortedHistoryItems = historyItemsCombined.OrderByDescending(historyItem => historyItem.CreatedDate ).ToList();

            CheckNewItems(sortedHistoryItems,5);

            return sortedHistoryItems; 
        }

        private void CheckNewItems(List<HistoryItemDto> historyItems, int minutes)
        {
            var currentDate = DateTime.UtcNow;

            foreach (var historyItem in historyItems)
            {
                var itemDate = DateTime.Parse(historyItem.CreatedDate!);

                var timeDifference = currentDate - itemDate;

                if (timeDifference < TimeSpan.FromMinutes(minutes))
                {
                    historyItem.IsNew = true;
                }
            }
        }
        private List<HistoryItemDto> MapHistoryEvents(IEnumerable<TicketHistory> historyEvents)
        {
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

            return historyItems;
        }
        private void AddHistoryItemType(IEnumerable<HistoryItemDto> historyItems, HistoryItem historyItemType)
        {
            foreach (var historyItem in historyItems)
            {
                historyItem.ItemType = historyItemType;
            }

        }
    }
}
