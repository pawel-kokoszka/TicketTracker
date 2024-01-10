using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketTypesForProjectConfigurationId
{
    internal class GetTicketTypesForProjectConfigurationIdQueryHandler : IRequestHandler<GetTicketTypesForProjectConfigurationIdQuery, IEnumerable<TicketTypeDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetTicketTypesForProjectConfigurationIdQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketTypeDto>> Handle(GetTicketTypesForProjectConfigurationIdQuery request, CancellationToken cancellationToken)
        {
            var ticketTypes = await _projConfRepository.GetTicektTypesForProjectConfigurationId(request.ProjectConfigurationId);
            var ticketTypesDtos = _mapper.Map<IEnumerable<TicketTypeDto>>(ticketTypes);

            return ticketTypesDtos;
        }
    }
}
