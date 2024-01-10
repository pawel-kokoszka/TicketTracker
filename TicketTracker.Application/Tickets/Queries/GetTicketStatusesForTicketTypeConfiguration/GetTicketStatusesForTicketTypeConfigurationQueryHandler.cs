using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketStatusesForTicketTypeConfiguration
{
    internal class GetTicketStatusesForTicketTypeConfigurationQueryHandler : IRequestHandler<GetTicketStatusesForTicketTypeConfigurationQuery, IEnumerable<TicketStatusDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetTicketStatusesForTicketTypeConfigurationQueryHandler(IProjectConfigurationRepository projConfRepository,IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketStatusDto>> Handle(GetTicketStatusesForTicketTypeConfigurationQuery request, CancellationToken cancellationToken)
        {
            var ticketStatuses = await _projConfRepository.GetTicketStatusesForTicketTypeConfigurationId(request.TicketTypeConfigurationId, request.StatusId);
            var ticketStatusesDtos = _mapper.Map<IEnumerable<TicketStatusDto>>(ticketStatuses);

            return ticketStatusesDtos;
        }
    }
}
