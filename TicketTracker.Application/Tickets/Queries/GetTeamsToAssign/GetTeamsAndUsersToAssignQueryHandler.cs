using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTeamsToAssign
{
    public class GetTeamsAndUsersToAssignQueryHandler : IRequestHandler<GetTeamsAndUsersToAssignQuery, List<TicketTracker.Domain.DTOs.TeamDto>>
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTeamsAndUsersToAssignQueryHandler(IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<List<TicketTracker.Domain.DTOs.TeamDto>> Handle(GetTeamsAndUsersToAssignQuery request, CancellationToken cancellationToken)
        {
            var teams = new List<TicketTracker.Domain.DTOs.TeamDto>();


            if (request.UserId != null)
            {
                 teams = await _projectConfigurationRepository.GetTeamsAndUsersToAssign(request.TicketTypeConfigurationId, request.UserId);

            }
            else
            {
                if (request.AssigningTeamId != null)
                {
                     teams = await _projectConfigurationRepository.GetTeamsAndUsersToAssign(request.TicketTypeConfigurationId, request.AssigningTeamId);
                }
            }














            //var teams = await _projectConfigurationRepository.GetTeamsAndUsersToAssign(request.TicketTypeConfigurationId, request.UserId);

            //var teamsDtos = _mapper.Map<List<TeamDto>>(teams);

            return teams;
        }
    }
}
