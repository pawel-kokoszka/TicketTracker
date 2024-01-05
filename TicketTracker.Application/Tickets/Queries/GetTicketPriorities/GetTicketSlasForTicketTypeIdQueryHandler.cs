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
    internal class GetTicketSlasForTicketTypeIdQueryHandler : IRequestHandler<GetTicketSlasForTicketTypeIdQuery, IEnumerable<TicketSlaDto>>
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTicketSlasForTicketTypeIdQueryHandler(IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper )
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketSlaDto>> Handle(GetTicketSlasForTicketTypeIdQuery request, CancellationToken cancellationToken)
        {
            var slas = await _projectConfigurationRepository.GetTicketSlasForTicketTypeId(request.TicketTypeConfigurationId);

            var ticketSlaDtos = _mapper.Map<IEnumerable<TicketSlaDto>>(slas);

            return ticketSlaDtos;
        }
    }
}
