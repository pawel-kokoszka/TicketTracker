using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetUserNameByUserId
{
    internal class GetUserNameByUserIdQueryHandler : IRequestHandler<GetUserNameByUserIdQuery, UserNameDto>
    {
        private readonly IUserRepsitory _userRepository;
        private readonly IMapper _mapper;

        public GetUserNameByUserIdQueryHandler(IUserRepsitory userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserNameDto> Handle(GetUserNameByUserIdQuery request, CancellationToken cancellationToken)
        {
            var searchedUser = await _userRepository.GetUserNameByUserId(request.UserId!);

            var userName = new UserNameDto();

            userName.UserId = searchedUser.Id;
            userName.UserName = searchedUser.UserName;

            return userName;
        }
    }
}
