using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetEnvironmentsForProjectId
{
    internal class GetEnvironmentsForProjectIdQueryHandler : IRequestHandler<GetEnvironmentsForProjectIdQuery, IEnumerable<EnvironmentDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetEnvironmentsForProjectIdQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnvironmentDto>> Handle(GetEnvironmentsForProjectIdQuery request, CancellationToken cancellationToken)
        {
            var environments = await _projConfRepository.GetEnvironmentsForProjectId(request.ProjectId, request.UserId, request.RequiredRoles);
            var environmentsDtos = _mapper.Map<IEnumerable<EnvironmentDto>>(environments);

            return environmentsDtos;
        }
    }
}
