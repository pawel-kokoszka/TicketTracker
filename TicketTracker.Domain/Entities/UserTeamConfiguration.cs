namespace TicketTracker.Domain.Entities
{
    public class UserTeamConfiguration
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        
        public int TeamTypeId { get; set; }
        public string? Description { get; set; }

        public string? UserId { get; set; }

        public int  DisplayOrder { get; set;}

        //ef
        public TeamType? TeamType { get; set; }

        public ApplicationUser? User { get; set; }

        public List<TicketTypeTeamAssignRule>? TicketTypeTeamAssignRules { get; set; } 


    }
}
