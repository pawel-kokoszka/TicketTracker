using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetUserNameByUserId
{
    public class GetUserNameByUserIdQuery : IRequest<UserNameDto>
    {
        public string? UserId {  get; set; }
        public GetUserNameByUserIdQuery(string? userId)
        {
            UserId = userId;
        }




    }
}
