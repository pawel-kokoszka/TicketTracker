﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Application.Tickets.Commands.CreateTicket;
using TicketTracker.Application.Tickets.Queries.GetTicketById;
using AutoMapper;
using TicketTracker.Application.Tickets.Commands.EditTicket;
using Microsoft.AspNetCore.Authorization;
using TicketTracker.MVC.Extensions;
using TicketTracker.Application.Comments.Commands;
using TicketTracker.Application.Comments.Queries.GetTicketComments;
using TicketTracker.Application.Tickets.Queries.GetTicketPriorities;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTracker.Application.Tickets.Queries.GetAllProjects;
using TicketTracker.Application.Tickets.Queries.GetEnvironmentsForProjectId;
using Microsoft.CodeAnalysis;
using TicketTracker.Application.Tickets.Queries.GetTicketTypesForProjectConfigurationId;
using TicketTracker.Application.Tickets.Queries.GetTicketStatusesForTicketTypeConfiguration;
using TicketTracker.Application.Tickets.Queries.GetTicketServices;
using TicketTracker.Application.Tickets.Queries.GetUserGroup;
using TicketTracker.Application.Tickets.Queries.GetTeamsToAssign;
using TicketTracker.Application.Tickets.Queries.GetAssigningTeam;
using TicketTracker.Application.Tickets.Queries.GetProjectsForUserId;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using TicketTracker.Domain.Entities;

namespace TicketTracker.MVC.Controllers
{
    [Authorize]
    public class TicketTrackerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly int _readOnlyTicketRole = 1;
        private readonly int _createTicketRole = 2;
        private readonly int _workOnTicketRole = 3;
        private readonly int _CommentAddOnlyTicketRole = 4;

        public TicketTrackerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            
            return View(tickets);
        }

        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        public async Task<IActionResult> AccessDenied()
        {
            //var tickets = await _mediator.Send(new GetAllTicketsQuery());
            ViewBag.AccessDeniedMessage = "Sorry, you don't have permisson to access this resource :(";

            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return View("Index",tickets);
        }

        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        [Route("TicketTracker/Details/{ticketId}")]
        public async Task<IActionResult> Details(int ticketId) 
        {
            //1 spr. usera
            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());

            //2 spr. role usera bazując na ticketId 
            //var currentUserRoles = await _mediator.Send(new GetUserRolesRelatedToTicketIdQuery(ticketId));
            var currentUserRoles = new List<int> { };//{_readOnlyTicketRole, _createTicketRole, _workOnTicketRole, _CommentAddOnlyTicketRole};
            
            var ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));

            if (currentUserRoles.IsNullOrEmpty())
            {
                //ViewBag.AccessDeniedMessage = "Sorry, you don't have permisson to access this resource :(";
                return RedirectToAction(nameof(AccessDenied));
            }
            else
            {
                if (currentUserRoles.Contains(_readOnlyTicketRole))
                {
                    ticketDetailsDto.IsEditable = false;
                }
                if (currentUserRoles.Contains(_workOnTicketRole))
                {
                    ticketDetailsDto.IsEditable = true;
                }

            }



            //3 jeśli nie znajduje żadnej roli związanej z dostępem do tt w tym widoku to przekierowuje do Index view z ViewBag.Message że brak uprawnień 


            return View(ticketDetailsDto);
        }

        [Authorize(Roles = "App User,Admin")]
        [Route("TicketTracker/Edit/{ticketId}")]
        public async Task<IActionResult> Edit(int ticketId)
        {
            var ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));

            EditTicketCommand command = _mapper.Map<EditTicketCommand>(ticketDetailsDto);
                        
            var ticketSlas = await _mediator.Send(new GetTicketSlasForTicketTypeIdQuery(command.TicketTypeConfigurationId));

            command.TicketSlaDtos = ticketSlas.ToList();

            var ticketStatuses = await _mediator.Send(new GetTicketStatusesForTicketTypeConfigurationQuery(command.TicketTypeConfigurationId, 
                                                                                                           command.TicketStatusId));
            command.TicketStatusDtos = ticketStatuses.ToList(); 

            return View(command);
        }

        [Authorize(Roles = "App User,Admin")]
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


        [Authorize(Roles = "App User,Admin")]
        //[Route("TicketTracker/CreateTicket")]
        public async Task<IActionResult> CreateTicket()
        {
            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());
            
            var userProjects = await _mediator.Send(new GetProjectsForUserIdQuery(currentUser.UserId, new List<int>{ 2,3}));

            CreateTicketCommand command = new CreateTicketCommand();

            if (userProjects.Count() > 0)
            {
                command.IsEditable = true;
            }
            else
            {
                command.IsEditable = false;
            }

            command.CreatedByUserId = currentUser.UserId;

            //var projects = await _mediator.Send(new GetAllProjectsQuery());
            ViewBag.Projects = userProjects.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name, });

            
            return View(command);
        }

        [Authorize(Roles = "App User,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created Ticket: {command.ShortDescription}");

            return RedirectToAction(nameof(Index)); 
        }



        [Authorize(Roles = "App User,Admin")]        
        public async Task<IActionResult> GetEnvironments(int projectId, string userId)
        {
            

            var environments = await _mediator.Send(new GetEnvironmentsForProjectIdQuery(projectId, userId, new List<int> { 2, 3 }));
            /*
             trzeba zmienić zwracany typ z zapytania żeby zwracał projectConfigurationID bo nie jest i w trakcie mapowania dto ma tam zera
             */

            return Json(environments);
        }

        [Authorize(Roles = "App User,Admin")]
        public async Task<IActionResult> GetTicketTypes(int projectConfiguratonId, string userId)
        {


            var ticketTypes = await _mediator.Send(new GetTicketTypesForProjectConfigurationIdQuery(projectConfiguratonId, userId, new List<int> { 2, 3 }));


            return Json(ticketTypes);
        }

        [Authorize(Roles = "App User,Admin")]
        public async Task<IActionResult> GetTicketSlas(int ticketTypeConfigurationId)
        {


            var ticketSlas = await _mediator.Send(new GetTicketSlasForTicketTypeIdQuery(ticketTypeConfigurationId));


            return Json(ticketSlas);
        }

        [Authorize(Roles = "App User,Admin")]
        public async Task<IActionResult> GetTicketServices(int ticketTypeConfigurationId)
        {


            var ticketServices = await _mediator.Send(new GetTicketServicesForTicketTypeIdQuery(ticketTypeConfigurationId));

            
            return Json(ticketServices);
        }

        [HttpGet]
        [Authorize(Roles = "App User,Admin")]
        [Route("TicketTracker/GetTeamsToAssign")] 
        public async Task<IActionResult> GetTeamsToAssign(int ticketTypeConfigurationId, string userId)
        {
            //var assigningTeam = _mediator.Send(new GetAssigningTeamQuery( ticketTypeConfigurationId, userId));

            //int val = assigningTeam.Result;

            var teamsToAssign = await _mediator.Send(new GetTeamsToAssignQuery(ticketTypeConfigurationId, userId));

            //teamsToAssign.ToList();

            return Json(teamsToAssign);
        }


        [HttpGet]
        [Authorize(Roles = "App User,Admin")]
        [Route("TicketTracker/GetAssigningTeam")] 
        public async Task<IActionResult> GetAssigningTeam(int ticketTypeConfigurationId, string userId)
        {            
            //trzeba dodać zabezpieczenie przed pustym wynikiem zapytania
            var assigningTeamId = await _mediator.Send(new GetAssigningTeamQuery(ticketTypeConfigurationId, userId));
            
            return Json(assigningTeamId);
        }


        [HttpPost]
        [Authorize(Roles = "App User,Admin")]
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
        [Authorize(Roles = "App User,Admin")]
        [Route("TicketTracker/Comments/{ticketId}")]
        public async Task<IActionResult> GetTicketComments(int ticketId)
        {
            var commentsData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId });

            return Ok(commentsData);
        }
        

    }
}
