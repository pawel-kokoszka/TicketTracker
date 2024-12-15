using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetUserRolesRelatedToTicketId
{
    internal class GetUserRolesRelatedToTicketIdQueryHandler : IRequestHandler<GetUserRolesRelatedToTicketIdQuery, UserRolesDto>
    {
        private readonly IProjectConfigurationRepository _projConfRepository;
        private readonly ITicketRepository _ticketRepository;        
        private readonly IMapper _mapper;


        public GetUserRolesRelatedToTicketIdQueryHandler(IProjectConfigurationRepository projConfRepository, ITicketRepository ticketRepository, IMapper mapper)
        {
            _projConfRepository = projConfRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<UserRolesDto> Handle(GetUserRolesRelatedToTicketIdQuery request, CancellationToken cancellationToken)
        {
            var ticketTypeConfigurationId = await _ticketRepository.GetTicketTypeConfigurationIdByTicketId(request.TicketId);

            //var foundRoles = await _projConfRepository.GetUserRolesRelatedToTicketId(request.TicketId, request.UserId);

            var foundRoles = await _projConfRepository.GetUserRolesForUserByTicketTypeConfigurationId(ticketTypeConfigurationId, request.UserId);


            var userRolesDto = _mapper.Map<UserRolesDto>(foundRoles);
                        
            return userRolesDto;
        }
    }
}
