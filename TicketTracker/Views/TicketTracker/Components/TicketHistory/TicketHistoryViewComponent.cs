using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Comments.Queries.GetTicketComments;

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
            var commentsData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId });
            return View(commentsData);
        }
    }
}
