using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.DTO.Ticket;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Mappings
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile() 
        {
            CreateMap<TicketDto, Domain.Entities.Ticket>();

            CreateMap<Domain.Entities.Ticket, TicketDto>();

        }


    }
}
