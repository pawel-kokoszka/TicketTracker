using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.DTO.Ticket;
using TicketTracker.Application.Services;
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _ticketService.Create(ticket); 

            return RedirectToAction(nameof(Create)); //todo refactor
        }
    }
}
