using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetAllProjects
{
    internal class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projConfRepository.GetAllProjects();
            var projectsDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return projectsDtos;
        }
    }
}
