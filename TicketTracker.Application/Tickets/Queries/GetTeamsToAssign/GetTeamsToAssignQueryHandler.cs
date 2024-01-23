using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTeamsToAssign
{
    public class GetTeamsToAssignQueryHandler : IRequestHandler<GetTeamsToAssignQuery, IEnumerable<TeamDto>>
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTeamsToAssignQueryHandler(IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> Handle(GetTeamsToAssignQuery request, CancellationToken cancellationToken)
        {
            var teams = await _projectConfigurationRepository.GetTeamsToAssign(request.TicketTypeConfigurationId, request.UserId);

            var teamsDtos = _mapper.Map<IEnumerable<TeamDto>>(teams);

            return teamsDtos;
        }
    }
}
