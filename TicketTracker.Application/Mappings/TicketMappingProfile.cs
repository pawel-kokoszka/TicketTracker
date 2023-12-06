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

            CreateMap<Domain.Entities.Ticket, TicketDetailsDto>()
                .ForMember(dto => dto.DateCreated, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dto => dto.DateEdited, opt => opt.MapFrom(src => src.DateEdited.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dto => dto.TicketTypeName, opt => opt.MapFrom(src => src.TicketType.TypeName))
                .ForMember(dto => dto.PriorityValue, opt => opt.MapFrom(src => src.TicketPriority.PriorityValue))
                .ForMember(dto => dto.ProjectName, opt => opt.MapFrom(src => src.ProjectConfiguration.Project.Name))

                ;


            CreateMap<TicketDetailsDto,EditTicketCommand>();

            CreateMap<EditTicketCommand, Domain.Entities.Ticket>();


            CreateMap<CreateCommentDto, Domain.Entities.Comment>();

            CreateMap<Domain.Entities.Comment, CommentDetailsDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm")) );

            CreateMap<TicketPriority, TicketPriorityDto>();

            CreateMap<TicketType, TicketTypeDto>();
            
            CreateMap<ProjectConfiguration, ProjectConfigurationDto>()
                .ForMember(dto => dto.ProjectName, opt => opt.MapFrom(src => src.Project.Name));



        }


    }
}
