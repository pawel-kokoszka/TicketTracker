using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetTeamsToAssign;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetProjectsForUserId
{
    internal class GetProjectsForUserIdQueryHandler : IRequestHandler<GetProjectsForUserIdQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetProjectsForUserIdQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsForUserIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projConfRepository.GetProjectsForUserId(request.UserId, request.RequiredRoles);//RequiredRoles do refactoru

            var projectsDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return projectsDtos;
        }
    }
}
