using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetCreateTicketRequiredData
{
    internal class GetCreateTicketRequiredDataQueryHandler : IRequestHandler<GetCreateTicketRequiredDataQuery, CreateTicketViewRequiredDto>
    {
        private readonly IMapper _mapper;
        private readonly ITicketPriorityRepository _ticketPriorityRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;

        
        public GetCreateTicketRequiredDataQueryHandler(
                    ITicketPriorityRepository ticketPriorityRepository, ITicketTypeRepository ticketTypeRepository, IMapper mapper)
        {
            _ticketPriorityRepository = ticketPriorityRepository;
            _ticketTypeRepository = ticketTypeRepository;   
            _mapper = mapper;
        }
        public async Task<CreateTicketViewRequiredDto> Handle(GetCreateTicketRequiredDataQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _ticketPriorityRepository.GetAll();

            var types = await _ticketTypeRepository.GetAll();

            var createTicketViewRequiredData = new CreateTicketViewRequiredDto { TicketPriorities = priorities, TicketTypes = types, 
                    CreateDto = null //new TicketCreateDto() 
                    //{
                    //    Description = null,
                    //    ShortDescription = null
                    //} 
            };

            return createTicketViewRequiredData;
        }
    }
}
