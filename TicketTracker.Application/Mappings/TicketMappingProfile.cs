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
                .ForMember(dto => dto.TicketSlaConfigurationName, opt => opt.MapFrom(src => src.TicketSlaConfigurations.Name))
                .ForMember(dto => dto.ProjectName, opt => opt.MapFrom(src => src.ProjectConfiguration.Project.Name))
                .ForMember(dto => dto.EnvironmentType, opt => opt.MapFrom(src => src.ProjectConfiguration.Environment.EnvironmentType.Name))
                .ForMember(dto => dto.EnvironmentName, opt => opt.MapFrom(src => src.ProjectConfiguration.Environment.Name))
                .ForMember(dto => dto.CreatedByUserId, opt => opt.MapFrom(src => src.CreatorUser.Id))
                .ForMember(dto => dto.CreatedByUserName, opt => opt.MapFrom(src => src.CreatorUser.UserName))
                .ForMember(dto => dto.EditedByUserId, opt => opt.MapFrom(src => src.EditorUser.Id))
                .ForMember(dto => dto.EditedByUserName, opt => opt.MapFrom(src => src.EditorUser.UserName))
                .ForMember(dto => dto.AssignedToUserId, opt => opt.MapFrom(src => src.AssignedUser.Id))
                .ForMember(dto => dto.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedUser.UserName))
                .ForMember(dto => dto.TicketStatusId, opt => opt.MapFrom(src => src.TicketStatusId))
                .ForMember(dto => dto.TicketStatusName, opt => opt.MapFrom(src => src.TicketStatuses.Name))

                ;

            //CreateMap<>
            //CreateMap<>


            CreateMap<TicketDetailsDto,EditTicketCommand>(); //used in Edit action for single ticket

            CreateMap<EditTicketCommand, Domain.Entities.Ticket>();

            


            CreateMap<CreateCommentDto, Domain.Entities.Comment>();

            CreateMap<Domain.Entities.Comment, CommentDetailsDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm")) );

           

            CreateMap<Domain.DTOs.TicketTypeDto, TicketTypeDto>();
            
            CreateMap<ProjectConfiguration, ProjectConfigurationDto>()
                .ForMember(dto => dto.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dto => dto.EnvironmentId, opt => opt.MapFrom(src => src.EnvironmentId))
                .ForMember(dto => dto.EnvironmentName, opt => opt.MapFrom(src => src.Environment.Name))
                .ForMember(dto => dto.EnvironmentType, opt => opt.MapFrom(src => src.Environment.EnvironmentType.Name))
                
                ;
            CreateMap<Project, ProjectDto>();

            CreateMap<Domain.Entities.Environment, EnvironmentDto>()
                .ForMember(dto => dto.ProjectConfigurationId, opt => opt.MapFrom(src => src.ProjectConfiguration.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<TicketSlaConfiguration, TicketSlaDto>();

            CreateMap<TicketFlowConfiguration, TicketStatusDto>(); //do spr. czy to jest gdzieś używane
            
            CreateMap<TicketStatus, TicketStatusDto>()
                .ForMember(dto => dto.StatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name)); 



        }


    }
}
