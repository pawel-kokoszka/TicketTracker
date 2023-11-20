﻿using Microsoft.AspNetCore.Mvc;
using TicketTracker.Application.Tickets;
using TicketTracker.Domain.Entities;
using MediatR;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Application.Tickets.Commands.CreateTicket;
using TicketTracker.Application.Tickets.Queries.GetTicketById;
using AutoMapper;
using TicketTracker.Application.Tickets.Commands.EditTicket;
using Microsoft.AspNetCore.Authorization;
using TicketTracker.MVC.Models;
using Newtonsoft.Json;
using TicketTracker.MVC.Extensions;
using TicketTracker.Application.Comments.Commands;
using TicketTracker.Application.Comments.Queries.GetTicketComments;

namespace TicketTracker.MVC.Controllers
{
    [Authorize]
    public class TicketTrackerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TicketTrackerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());

            return View(tickets);
        }

        [Authorize(Roles = "Ticket Viewer,Admin")]
        [Route("TicketTracker/Details/{ticketId}")]
        public async Task<IActionResult> Details(int ticketId) 
        {
            var ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));

            return View(ticketDetailsDto);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        [Route("TicketTracker/Edit/{ticketId}")]
        public async Task<IActionResult> Edit(int ticketId)
        {
            var ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));

            EditTicketCommand command = _mapper.Map<EditTicketCommand>(ticketDetailsDto);

            return View(command);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        [HttpPost]
        [Route("TicketTracker/Edit/{ticketId}")]
        public async Task<IActionResult> Edit( EditTicketCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
                       
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        public IActionResult Create() 
        {
            return View();
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created Ticket: {command.ShortDescription}");

            return RedirectToAction(nameof(Index)); 
        }
        
        [HttpPost]
        [Authorize(Roles = "Ticket Maker,Admin")]
        [Route("TicketTracker/Comment")]
        public async Task<IActionResult> CreateComment(CreateCommentCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            
            return Ok();    
        }

        [HttpGet]
        [Route("TicketTracker/Comments/{ticketId}")]
        public async Task<IActionResult> GetTicketComments(int ticketId)
        {
            var commentsData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId });

            return Ok(commentsData);
        }
        
    }
}
