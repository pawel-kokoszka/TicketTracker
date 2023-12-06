using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetTicketPriorities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketTypes
{
    internal class GetTicketTypesQueryHandler : IRequestHandler<GetTicketTypesQuery, IEnumerable<TicketTypeDto>>
    {
        private readonly ITicketTypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public GetTicketTypesQueryHandler(ITicketTypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketTypeDto>> Handle(GetTicketTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetAll();
            var ticketTypesDtos = _mapper.Map<IEnumerable<TicketTypeDto>>(types);

            return ticketTypesDtos;
        }
    }
}
