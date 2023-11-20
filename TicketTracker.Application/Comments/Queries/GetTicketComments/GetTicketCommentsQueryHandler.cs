using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Comments.Queries.GetTicketComments
{
    public class GetTicketCommentsQueryHandler : IRequestHandler<GetTicketCommentsQuery, IEnumerable<CommentDetailsDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public GetTicketCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper )
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentDetailsDto>> Handle(GetTicketCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommmentsByTicketId(request.TicketId);

            var commentsDtos = _mapper.Map<IEnumerable<CommentDetailsDto>>(comments);

            return commentsDtos;
        }
    }
}
