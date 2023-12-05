using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetTicketTypes;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetAllProjectConfigurations
{
    internal class GetAllProjectConfigurationsQueryHandler : IRequestHandler<GetAllProjectConfigurationsQuery, IEnumerable<ProjectConfigurationDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;
        public GetAllProjectConfigurationsQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectConfigurationDto>> Handle(GetAllProjectConfigurationsQuery request, CancellationToken cancellationToken)
        {
            var projConfigs = await _projConfRepository.GetAllProjectConfigurations();
            var projConfDtos = _mapper.Map<IEnumerable<ProjectConfigurationDto>>(projConfigs);

            return projConfDtos;
        }
    }
}
