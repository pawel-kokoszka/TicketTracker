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
    internal class GetTicketPrioritiesForTicketTypeIdQueryHandler //: IRequestHandler<GetTicketPrioritiesForTicketTypeIdQuery, IEnumerable<TicketPriorityDto>>
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTicketPrioritiesForTicketTypeIdQueryHandler(IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper )
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<TicketPriorityDto>> Handle(GetTicketPrioritiesForTicketTypeIdQuery request, CancellationToken cancellationToken)
        //{
        //    //var priorities = await _projectConfigurationRepository.GetTicektPrioritiesForTicketTypeId(request.TicketTypeConfigurationId);

        //    //var ticketPriorityDtos = _mapper.Map<IEnumerable<TicketPriorityDto>>(priorities);

        //    return ticketPriorityDtos;
        //}
    }
}
