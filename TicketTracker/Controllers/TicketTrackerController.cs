using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Tickets;
using TicketTracker.Domain.Entities;
using MediatR;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Application.Tickets.Commands.CreateTicket;

namespace TicketTracker.MVC.Controllers
{
    public class TicketTrackerController : Controller
    {
        private readonly IMediator _mediator;
        public TicketTrackerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());

            return View(tickets);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index)); 
        }
    }
}
