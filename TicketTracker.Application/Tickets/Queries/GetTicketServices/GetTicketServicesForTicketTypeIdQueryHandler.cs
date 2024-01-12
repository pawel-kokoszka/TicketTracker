using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketServices
{
    internal class GetTicketServicesForTicketTypeIdQueryHandler :  IRequestHandler<GetTicketServicesForTicketTypeIdQuery, IEnumerable<TicketServiceDto>>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly IMapper _mapper;

        public GetTicketServicesForTicketTypeIdQueryHandler(IProjectConfigurationRepository projConfRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketServiceDto>> Handle(GetTicketServicesForTicketTypeIdQuery request, CancellationToken cancellationToken)
        {
            var ticketServices = await _projConfRepository.GetTicektServicesForTicketConfigurationId(request.TicketTypeConfigurationId);
            var ticketServicesDtos = _mapper.Map<IEnumerable<TicketServiceDto>>(ticketServices);

            return ticketServicesDtos;
        }
    }
}
