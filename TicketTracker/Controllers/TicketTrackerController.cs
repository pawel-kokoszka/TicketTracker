using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Services;
using TicketTracker.Application.Ticket;
using TicketTracker.Domain.Entities;

namespace TicketTracker.MVC.Controllers
{
    public class TicketTrackerController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketTrackerController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketDto ticket)
        {
            await _ticketService.Create(ticket); 

            return RedirectToAction(nameof(Create)); //todo refactor
        }
    }
}
