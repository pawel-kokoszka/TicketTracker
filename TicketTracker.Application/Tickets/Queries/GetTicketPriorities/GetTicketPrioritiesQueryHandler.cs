using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketPriorities
{
    internal class GetTicketPrioritiesQueryHandler : IRequestHandler<GetTicketPrioritiesQuery, IEnumerable<TicketPriorityDto>>
    {
        private readonly ITicketPriorityRepository _priorityRepository;
        private readonly IMapper _mapper;

        public GetTicketPrioritiesQueryHandler(ITicketPriorityRepository priorityRepository, IMapper mapper )
        {
            _priorityRepository = priorityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketPriorityDto>> Handle(GetTicketPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _priorityRepository.GetAll();

            var ticketPriorityDtos = _mapper.Map<IEnumerable<TicketPriorityDto>>(priorities);

            return ticketPriorityDtos;
        }
    }
}
