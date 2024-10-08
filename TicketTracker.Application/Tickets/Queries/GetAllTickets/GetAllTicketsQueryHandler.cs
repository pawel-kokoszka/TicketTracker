﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Queries.GetAllTickets;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetAllTickets
{
    internal class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDetailsDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository, ITicketTypeRepository ticketTypeRepository,IMapper mapper)
        {
            _ticketRepository = ticketRepository;            
            _ticketTypeRepository = ticketTypeRepository;   
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketDetailsDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAll();

            var ticketDtos = _mapper.Map<IEnumerable<TicketDetailsDto>>(tickets);

            return ticketDtos;
        }
    }
}
