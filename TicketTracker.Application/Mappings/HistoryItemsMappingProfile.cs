using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Comments;

namespace TicketTracker.Application.Mappings
{
    public class HistoryItemsMappingProfile : Profile
    {
        public HistoryItemsMappingProfile()
        {
            CreateMap<CreateCommentDto, Domain.Entities.Comment>();

            CreateMap<Domain.Entities.Comment, CommentDetailsDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm")));

            CreateMap<Domain.Entities.Comment, HistoryItemDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm")));

            CreateMap<Domain.Entities.TicketHistory, HistoryItemDto>()
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(src => src.DateEdited.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.User!.Email));
                
        }
    }
}
