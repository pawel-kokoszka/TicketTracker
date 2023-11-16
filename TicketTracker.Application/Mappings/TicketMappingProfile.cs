using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;
using TicketTracker.Application.Tickets;
using TicketTracker.Application.Tickets.Commands.EditTicket;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Application.Mappings
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile() 
        {
            CreateMap<TicketCreateDto, Domain.Entities.Ticket>();

            CreateMap<Domain.Entities.Ticket, TicketCreateDto>();

            CreateMap<Domain.Entities.Ticket, TicketDetailsDto>();

            CreateMap<TicketDetailsDto,EditTicketCommand>();

            CreateMap<EditTicketCommand, Domain.Entities.Ticket>();

            CreateMap<CreateCommentDto, Domain.Entities.Comment>();
        }


    }
}
