using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;
using TicketTracker.Application.Tickets;
using TicketTracker.Application.Tickets.Commands.CreateTicket;
using TicketTracker.Application.Tickets.Commands.EditTicket;
using TicketTracker.Domain.DTOs;
using TicketTracker.Domain.Entities;
using TicketStatusDto = TicketTracker.Application.Tickets.TicketStatusDto;

namespace TicketTracker.Application.Mappings
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile() 
        {
            CreateMap<TicketCreateDto, Domain.Entities.Ticket>();

            CreateMap<Domain.Entities.Ticket, TicketCreateDto>();

            //For passing Command to View with user group/team 
            CreateMap<TicketCreateDto, CreateTicketCommand>();
           

            CreateMap<Domain.Entities.Ticket, TicketDetailsDto>()
                .ForMember(dto => dto.DateCreated, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.DateEdited, opt => opt.MapFrom(src => src.DateEdited.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.TicketTypeName, opt => opt.MapFrom(src => src.TicketType.TypeName))
                .ForMember(dto => dto.TicketSlaConfigurationName, opt => opt.MapFrom(src => src.TicketSlaConfigurations.Name))
                .ForMember(dto => dto.ProjectName, opt => opt.MapFrom(src => src.ProjectConfiguration.Project.Name))
                .ForMember(dto => dto.EnvironmentType, opt => opt.MapFrom(src => src.ProjectConfiguration.Environment.EnvironmentType.Name))
                .ForMember(dto => dto.EnvironmentName, opt => opt.MapFrom(src => src.ProjectConfiguration.Environment.Name))
                .ForMember(dto => dto.CreatedByUserId, opt => opt.MapFrom(src => src.CreatorUser.Id))
                .ForMember(dto => dto.CreatedByUserName, opt => opt.MapFrom(src => src.CreatorUser.UserName))
                .ForMember(dto => dto.EditedByUserId, opt => opt.MapFrom(src => src.EditorUser.Id))
                .ForMember(dto => dto.EditedByUserName, opt => opt.MapFrom(src => src.EditorUser.UserName))
                .ForMember(dto => dto.TicketStatusId, opt => opt.MapFrom(src => src.TicketStatusId))
                .ForMember(dto => dto.TicketStatusName, opt => opt.MapFrom(src => src.TicketStatuses.Name))
                .ForMember(dto => dto.TicketServiceId, opt => opt.MapFrom(src => src.TicketService.Id))
                .ForMember(dto => dto.TicketServiceName, opt => opt.MapFrom(src => src.TicketService.ServiceName))
                .ForMember(dto => dto.TicketSubServiceId, opt => opt.MapFrom(src => src.TicketSubService.Id))
                .ForMember(dto => dto.TicketSubServiceName, opt => opt.MapFrom(src => src.TicketSubService.SubServiceName))
                .ForMember(dto => dto.AssignedToUserId, opt => opt.MapFrom(src => src.AssignedUser.Id))
                .ForMember(dto => dto.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedUser.UserName))
                .ForMember(dto => dto.AssignedTeamId, opt => opt.MapFrom(src => src.AssignedTeamId))
                .ForMember(dto => dto.AssignedTeamName, opt => opt.MapFrom(src => src.AssignedTeam.Name))
                .ForMember(dto => dto.AssigningTeamId, opt => opt.MapFrom(src => src.AssigningTeamId))
                .ForMember(dto => dto.AssigningTeamName, opt => opt.MapFrom(src => src.AssigningTeam.Name))
                
                
                //.ForMember(dto => dto., opt => opt.MapFrom(src => src.))

                ;

            //CreateMap<>
           


            CreateMap<TicketDetailsDto,EditTicketCommand>(); //used in Edit action for single ticket

            //to delete:
            //manual mappin used instead of:
            //CreateMap<Ticket, Ticket>(MemberList.None) //used in Edit action for single ticket oldTT <- newTT
            //    .ForMember(newTT => newTT.AssignedTeamId, oldTT => oldTT.MapFrom(src => src.AssignedTeamId) )
            //    .ForMember(newTT => newTT.AssignedUserId, oldTT => oldTT.MapFrom(src => src.AssignedUserId) )
            //    .ForMember(newTT => newTT.DateEdited, oldTT => oldTT.MapFrom(src => src.DateEdited) )
            //    .ForMember(newTT => newTT.Description, oldTT => oldTT.MapFrom(src => src.Description) )
            //    .ForMember(newTT => newTT.EditedByUserId, oldTT => oldTT.MapFrom(src => src.EditedByUserId) )
            //    .ForMember(newTT => newTT.IsDeleted, oldTT => oldTT.MapFrom(src => src.IsDeleted) )
            //    .ForMember(newTT => newTT.ShortDescription, oldTT => oldTT.MapFrom(src => src.ShortDescription) )
            //    .ForMember(newTT => newTT.TicketServiceId, oldTT => oldTT.MapFrom(src => src.TicketServiceId) )
            //    .ForMember(newTT => newTT.TicketSlaConfigurationId, oldTT => oldTT.MapFrom(src => src.TicketSlaConfigurationId) )
            //    .ForMember(newTT => newTT.TicketStatusId, oldTT => oldTT.MapFrom(src => src.TicketStatusId) )
            //    .ForMember(newTT => newTT.TicketSubServiceId, oldTT => oldTT.MapFrom(src => src.TicketSubServiceId) )
                
            //    //.ForAllOtherMembers(opt => opt.Ignore())
            //    ; 




            CreateMap<EditTicketCommand, Domain.Entities.Ticket>()
                .ForMember(entity => entity.AssignedUserId, opt => opt.MapFrom(src => src.AssignedToUserId) )
                ;

            


            CreateMap<CreateCommentDto, Domain.Entities.Comment>();

            CreateMap<Domain.Entities.Comment, CommentDetailsDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm")) );

           

            CreateMap<Domain.DTOs.TicketTypeDto, Application.Tickets.TicketTypeDto>();
            
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

            ///dodaj mapowanie do obu TicketStatusDto
            CreateMap<Domain.DTOs.TicketStatusDto, Application.Tickets.TicketStatusDto>();


            CreateMap<TicketStatus, TicketStatusDto>()
                .ForMember(dto => dto.StatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name)); 

            
            CreateMap<TicketService, TicketServiceDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.ServiceName , opt => opt.MapFrom(src => src.ServiceName))
                .ForMember(dto => dto.TicketSubServices , opt => opt.MapFrom(src => src.TicketSubServices));


            CreateMap<TicketSubService, TicketSubServiceDto>();

            CreateMap<Domain.DTOs.TeamDto, Application.Tickets.TeamDto>();

            CreateMap<IEnumerable<TeamRoleType>, UserRolesDto>()
                .ForMember(dto => dto.Read, opt => opt.MapFrom(src => src.Any(role => role.Name == "Read") ))
                .ForMember(dto => dto.Create, opt => opt.MapFrom(src => src.Any(role => role.Name == "Create") ))
                .ForMember(dto => dto.Edit, opt => opt.MapFrom(src => src.Any(role => role.Name == "Edit") ))
                .ForMember(dto => dto.Comment, opt => opt.MapFrom(src => src.Any(role => role.Name == "Comment") ))
                ;

        }


    }
}
