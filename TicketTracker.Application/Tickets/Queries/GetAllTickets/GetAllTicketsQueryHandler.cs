using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetAllTickets
{
    internal class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDetailsDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketPriorityRepository _ticketPriorityRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository, ITicketPriorityRepository ticketPriorityRepository, ITicketTypeRepository ticketTypeRepository,IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _ticketPriorityRepository = ticketPriorityRepository;
            _ticketTypeRepository = ticketTypeRepository;   
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketDetailsDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAll();
            var priorities = await _ticketPriorityRepository.GetAll();
            var types = await _ticketTypeRepository.GetAll();

            var ticketDtos = _mapper.Map<IEnumerable<TicketDetailsDto>>(tickets);

            foreach (var ticketDto in ticketDtos)
            {
                var prio = priorities.Where(p => p.Id == ticketDto.PriorityId).First();
                ticketDto.PriorityValue = prio.PriorityValue;
                //var type = types.Where(t => t.Id == ticketDto.TypeId).First();
                //ticketDto.TicketTypeName = type.TypeName;
            }

            

            return ticketDtos;
        }
    }
}
