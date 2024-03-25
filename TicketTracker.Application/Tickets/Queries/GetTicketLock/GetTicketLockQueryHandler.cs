using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetTicketById;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketLock
{
    internal class GetTicketLockQueryHandler : IRequestHandler<GetTicketLockQuery, TicketEditLockDetailsDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketLockQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<TicketEditLockDetailsDto> Handle(GetTicketLockQuery request, CancellationToken cancellationToken)
        {
            var ticketLockEntry = await _ticketRepository.GetTicketLockByTicketId(request.TicketId);

            if (ticketLockEntry == null)
            {
                TicketEditLockDetailsDto ticketLockDto = new TicketEditLockDetailsDto();

                ticketLockDto.IsLockNull = true;

                return ticketLockDto;
            }
            else
            {
                var ticketLockDto = _mapper.Map<TicketEditLockDetailsDto>(ticketLockEntry);

                return ticketLockDto;
            }



        }
    }
}
