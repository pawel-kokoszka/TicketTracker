using Microsoft.AspNetCore.Mvc;
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
using TicketTracker.Application.Tickets.Queries.GetTicketPriorities;
using TicketTracker.Application.Tickets.Queries.GetTicketTypes;
using TicketTracker.Application.Tickets.Queries.GetAllProjectConfigurations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTracker.Application.Tickets.Queries.GetAllProjects;
using TicketTracker.Application.Tickets.Queries.GetEnvironmentsForProjectId;
using Microsoft.CodeAnalysis;
using TicketTracker.Application.Tickets.Queries.GetTicketTypesForProjectConfigurationId;
using TicketTracker.Application.Tickets.Queries.GetTicketStatusesForTicketTypeConfiguration;
using TicketTracker.Application.Tickets.Queries.GetTicketServices;
using TicketTracker.Application.Tickets.Queries.GetUserGroup;

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

        [Authorize(Roles = "Ticket Viewer,Ticket Maker,Admin")]
        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());

            return View(tickets);
        }

        [Authorize(Roles = "Ticket Viewer,Ticket Maker,Admin")]
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
                        
            var ticketSlas = await _mediator.Send(new GetTicketSlasForTicketTypeIdQuery(command.TicketTypeConfigurationId));

            command.TicketSlaDtos = ticketSlas.ToList();

            var ticketStatuses = await _mediator.Send(new GetTicketStatusesForTicketTypeConfigurationQuery(command.TicketTypeConfigurationId, 
                                                                                                           command.TicketStatusId));
            command.TicketStatusDtos = ticketStatuses.ToList(); 

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

        //do wywalenia - nieużywane
        //[Authorize(Roles = "Ticket Maker,Admin")]
        //public async Task<IActionResult> Create() 
        //{
        //    //var ticketPrioritieDtos = await _mediator.Send(new GetTicketPrioritiesForTicketTypeIdQuery ());
        //    //ViewData[nameof(ticketPrioritieDtos)] = ticketPrioritieDtos;
            
        //    var ticketTypeDtos = await _mediator.Send(new GetTicketTypesQuery ());
        //    ViewData[nameof(ticketTypeDtos)] = ticketTypeDtos;
            
        //    //var projectConfigurations = await _mediator.Send(new GetAllProjectConfigurationsQuery());
        //    //ViewData[nameof(projectConfigurations)] = projectConfigurations;




        //    return View();
        //}




        [Authorize(Roles = "Ticket Maker,Admin")]
        //[Route("TicketTracker/CreateTicket")]
        public async Task<IActionResult> CreateTicket()
        {
            //var ticketCreateDto = new TicketCreateDto ();

            CreateTicketCommand command = new CreateTicketCommand();           
            var currentUser = await _mediator.Send(new GetCurrentUserIdQuery());
            command.CreatedByUserId = currentUser.UserId;

            var projects = await _mediator.Send(new GetAllProjectsQuery());
            ViewBag.Projects = projects.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name, });

            
            return View(command);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
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



        [Authorize(Roles = "Ticket Maker,Admin")]        
        public async Task<IActionResult> GetEnvironments(int projectId)
        {
            

            var environments = await _mediator.Send(new GetEnvironmentsForProjectIdQuery(projectId));


            return Json(environments);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        public async Task<IActionResult> GetTicketTypes(int projectConfiguratonId)
        {


            var ticketTypes = await _mediator.Send(new GetTicketTypesForProjectConfigurationIdQuery(projectConfiguratonId));


            return Json(ticketTypes);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        public async Task<IActionResult> GetTicketSlas(int ticketTypeConfigurationId)
        {


            var ticketSlas = await _mediator.Send(new GetTicketSlasForTicketTypeIdQuery(ticketTypeConfigurationId));


            return Json(ticketSlas);
        }

        [Authorize(Roles = "Ticket Maker,Admin")]
        public async Task<IActionResult> GetTicketServices(int ticketTypeConfigurationId)
        {


            var ticketServices = await _mediator.Send(new GetTicketServicesForTicketTypeIdQuery(ticketTypeConfigurationId));

            
            return Json(ticketServices);
        }


        //nie uzywane do wyrzucenia, pierwszy status jest nadawany w CreateTicketCommandHandler
        //[Authorize(Roles = "Ticket Maker,Admin")]
        //public async Task<IActionResult> GetTicketStatuses(int ticketTypeConfigurationId, int currentStatus)
        //{


        //    var ticketSlas = await _mediator.Send(new GetTicketStatusesForTicketTypeConfigurationQuery(ticketTypeConfigurationId, currentStatus));


        //    return Json(ticketSlas);
        //}
























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
        [Authorize(Roles = "Ticket Maker,Admin")]
        [Route("TicketTracker/Comments/{ticketId}")]
        public async Task<IActionResult> GetTicketComments(int ticketId)
        {
            var commentsData = await _mediator.Send(new GetTicketCommentsQuery() { TicketId = ticketId });

            return Ok(commentsData);
        }
        

    }
}
