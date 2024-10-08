﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets.Queries.GetEnvironmentsForProjectId
{
    public class GetEnvironmentsForProjectIdQuery : IRequest<IEnumerable<EnvironmentDto>>
    {
        public int ProjectId { get; set; }
        public string? UserId { get; set; }
        public List<int> RequiredRoles { get; set; }
        public GetEnvironmentsForProjectIdQuery(int projectId, string? userId, List<int> requiredRoles)
        {
            ProjectId = projectId;
            UserId = userId;
            RequiredRoles = requiredRoles;
        }

    }
}
