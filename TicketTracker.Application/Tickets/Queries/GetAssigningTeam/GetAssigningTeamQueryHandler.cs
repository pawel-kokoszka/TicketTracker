using AutoMapper;
using MediatR;
using TicketTracker.Application.Tickets.Queries.GetTeamsToAssign;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetAssigningTeam
{
    public class GetAssigningTeamQueryHandler : IRequestHandler<GetAssigningTeamQuery, int>
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetAssigningTeamQueryHandler(IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetAssigningTeamQuery request, CancellationToken cancellationToken)
        {
            //trzeba dodać zabezpieczenie przed pustym wynikiem zapytania bo rzuca wyjątkiem
            var assigningTeamId = await _projectConfigurationRepository.GetAssigningTeam(request.TicketTypeConfigurationId, request.UserId);

            return assigningTeamId; 
        }
    }
}
