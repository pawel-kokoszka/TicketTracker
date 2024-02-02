using AutoMapper;
using MediatR;
using System.Security.Principal;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Application.Tickets.Queries.GetUserGroup;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetUserId
{
    internal class GetCurrentUserIdQueryHandler : IRequestHandler<GetCurrentUserIdQuery, UserIdDto>
    {
        //private readonly IUserRepsitory _userRepsitory; to nie jest potrzebne 
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        

        public GetCurrentUserIdQueryHandler(/*IUserRepsitory userRepsitory,*/ IUserContext userContext, IMapper mapper)
        {
            //_userRepsitory = userRepsitory;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<UserIdDto> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser =  _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                //await Task.FromCanceled(cancellationToken);   //pewnie do refactoru
                throw new InvalidOperationException();    
            }
                        

            UserIdDto user = new UserIdDto();
            user.UserId = currentUser.Id;

            return user;  
        }
    }
}
