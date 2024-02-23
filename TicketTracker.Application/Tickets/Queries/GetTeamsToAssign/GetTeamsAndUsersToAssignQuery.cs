using MediatR;

namespace TicketTracker.Application.Tickets.Queries.GetTeamsToAssign
{
    public class GetTeamsAndUsersToAssignQuery : IRequest<List<TicketTracker.Domain.DTOs.TeamDto>>
    {
        public int TicketTypeConfigurationId {  get; set; }
        public string? UserId { get; set; }
        public int? AssigningTeamId { get; set; }

        public GetTeamsAndUsersToAssignQuery(int ticketTypeConfigurationId, string userId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
            UserId = userId;
        }

        public GetTeamsAndUsersToAssignQuery(int ticketTypeConfigurationId, int assigningTeamId)
        {
            TicketTypeConfigurationId = ticketTypeConfigurationId;
            AssigningTeamId = assigningTeamId;
        }


    }
}
