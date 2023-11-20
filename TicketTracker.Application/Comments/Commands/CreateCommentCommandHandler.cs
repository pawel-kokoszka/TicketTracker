using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Comments.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly IUserContext _userContext;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IUserContext userContext, ICommentRepository commentRepository, IMapper mapper)
        {
            _userContext = userContext;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Ticket Maker")))
            {
                return Unit.Value;
            }

            //var comment = new Comment();
            //{
            //    Message = request.Message,
            //    UserId = currentUser.Id,
            //    CreatedDate = DateTime.UtcNow,
            //    TicketId = 1
            //};

            var comment = _mapper.Map<Comment>(request);

            comment.TicketId = request.TicketId;
            comment.CreatedDate = DateTime.Now; 
            comment.UserId = currentUser.Id;
            



            await _commentRepository.Create(comment);

            return Unit.Value;
        }
    }
}
