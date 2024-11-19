using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Comments.Queries.GetTicketComments;
using TicketTracker.Application.HistoryItems.Queries;

namespace TicketTracker.MVC.Views.TicketTracker.Components.TicketHistory
{
    public class TicketHistoryViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TicketHistoryViewComponent(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    

        public async Task<IViewComponentResult> InvokeAsync(int ticketId)
        {
            var commentsData = await _mediator.Send(new GetHistoryItemsQuery() { TicketId = ticketId });

            //var historyData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId+1 });
            //var combinedData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId+2 });



            return View(commentsData);
        }
    }


}
