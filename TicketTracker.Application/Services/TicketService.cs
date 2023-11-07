using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.DTO.Ticket;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRpository;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRpository = ticketRepository;
            _mapper = mapper;
        }
        public async Task Create(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<Domain.Entities.Ticket>(ticketDto);

            await _ticketRpository.Create(ticket);
        }
    }
}
