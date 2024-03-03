using Microsoft.AspNetCore.Mvc;
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
using System.Linq;
using TicketTracker.Application.Tickets.Queries.GetUserRolesRelatedToTicketId;
using TicketTracker.Application.Tickets;

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

        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            
            return View(tickets);
        }






        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        public async Task<IActionResult> AccessDenied()
        {            
            ViewBag.AccessDeniedMessage = "Sorry, you don't have permisson to access this resource :(";

            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return View("Index",tickets);
        }


        [Authorize(Roles = "App User,Ticket Maker,Admin")]
        [Route("TicketTracker/Details/{ticketId}")]
        public async Task<IActionResult> Details(int ticketId) 
        {     

            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());

            var foundUserRoles = await _mediator.Send(new GetUserRolesRelatedToTicketIdQuery(ticketId, currentUser.UserId));

            TicketDetailsDto? ticketDetailsDto = null;



            if (foundUserRoles.Read == false && foundUserRoles.Edit == false)
            {                
                return RedirectToAction(nameof(AccessDenied));
            }
            else
            {
                ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));
                
                if (foundUserRoles.Read == true)                
                {
                    ticketDetailsDto.IsEditable = false;
                }
                else
                {
                    if (foundUserRoles.Edit == true)
                    {
                        ticketDetailsDto.IsEditable = true;
                    }
                }
                
                if (foundUserRoles.Comment == true || foundUserRoles.Edit == true)
                {
                    ticketDetailsDto.IsAbleToComment = true;
                }
            }
                        
            return View(ticketDetailsDto);
        }


        [Authorize(Roles = "App User,Admin")]
        [Route("TicketTracker/Edit/{ticketId}")]
        public async Task<IActionResult> Edit(int ticketId)
        {
            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());

            var foundUserRoles = await _mediator.Send(new GetUserRolesRelatedToTicketIdQuery(ticketId, currentUser.UserId));

            TicketDetailsDto? ticketDetailsDto = null;
            

            if (foundUserRoles.Edit == false)//1
            {
                return RedirectToAction(nameof(AccessDenied));
            }
            else
            {                   
                ticketDetailsDto = await _mediator.Send(new GetTicketByIdQuery(ticketId));
                
                ticketDetailsDto.IsEditable = true;//2
                
                                
                if (foundUserRoles.Edit == true)
                {
                    ticketDetailsDto.IsAbleToComment = true;
                }

                
                if (   currentUser.TeamsList.Contains((int)ticketDetailsDto.AssigningTeamId) 
                    && currentUser.TeamsList.Contains((int)ticketDetailsDto.AssignedTeamId)   )
                {
                    //cu jest twórcą i assigned to tt
                    ticketDetailsDto.IsUserInCreatorTeam= true;
                    ticketDetailsDto.IsUserInAssignedTeam= true;

                }
                else
                {
                    if (       currentUser.TeamsList.Contains((int)ticketDetailsDto.AssignedTeamId)
                            && !currentUser.TeamsList.Contains((int)ticketDetailsDto.AssigningTeamId) )
                    {
                        //cu jest assigned do tt
                        ticketDetailsDto.IsUserInAssignedTeam = true;
                    }
                    else
                    {
                        if (    currentUser.TeamsList.Contains((int)ticketDetailsDto.AssigningTeamId) 
                             && !currentUser.TeamsList.Contains((int)ticketDetailsDto.AssignedTeamId))
                        {
                            //cu jest twórcą tt
                            ticketDetailsDto.IsUserInCreatorTeam = true;
                        }
                        else
                        {
                            ticketDetailsDto.IsUserInEditOnlyTeam = true; //user can only edit tt properties unrelated to tt status
                        }              

                    }
                }
               
            }

            EditTicketCommand command = _mapper.Map<EditTicketCommand>(ticketDetailsDto);
                        
            
            var ticketSlas = await _mediator.Send(new GetTicketSlasForTicketTypeIdQuery(command.TicketTypeConfigurationId));

            command.TicketSlaDtos = ticketSlas.ToList();


            var ticketStatuses = await _mediator.Send(new GetTicketStatusesForTicketTypeConfigurationQuery(command.TicketTypeConfigurationId, 
                                                                                                            command.TicketStatusId));
            command.TicketStatusDtos = ticketStatuses.ToList();


            var teamsAndUsersToAssign = await _mediator.Send(new GetTeamsAndUsersToAssignQuery(command.TicketTypeConfigurationId, command.AssigningTeamId));

            command.UsersToAssign = teamsAndUsersToAssign.ToList();


            return View(command);
        }

        [Authorize(Roles = "App User,Admin")]
        [HttpPost]
        [Route("TicketTracker/Edit/{ticketId}")]
        public async Task<IActionResult> Edit( EditTicketCommand command)
        {
            //todo - dodaj sprawdzanie ról 



            if (!ModelState.IsValid)
            {
                return View(command);
            }
                       
            await _mediator.Send(command);
            
           return RedirectToAction("Details",new { ticketId = command.Id });          
        }


        [Authorize(Roles = "App User,Admin")]
        //[Route("TicketTracker/CreateTicket")]
        public async Task<IActionResult> CreateTicket()
        {
            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());
            
            var userProjects = await _mediator.Send(new GetProjectsForUserIdQuery(currentUser.UserId, new List<int>{ 2,3})); //to do: zmień żeby używane było GetUserRolesRelatedToTicketIdQuery

            CreateTicketCommand command = new CreateTicketCommand();

            if (userProjects.Count() > 0)
            {
                command.IsEditable = true;
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
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

            if (command.AssignedUserId == "0")
            {
                command.AssignedUserId = null;
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created Ticket: {command.ShortDescription}");

            return RedirectToAction(nameof(Index));
            //to do: add redirection do details view 
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

            var teamsToAssign = await _mediator.Send(new GetTeamsAndUsersToAssignQuery(ticketTypeConfigurationId, userId));

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
