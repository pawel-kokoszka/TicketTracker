namespace TicketTracker.Domain.Entities
{
    public class TeamsUsers
    {
        public int TeamId { get; set; } //pk
        public string? UserId { get; set; }//pk

        public int  SeniorityOrder { get; set;}

        //ef
        public ApplicationUser? ApplicationUser { get; set; }
        public Team? Team { get; set; }
        
        //public TeamType? TeamType { get; set; }


        //public List<TicketTypeTeamAssignRule>? TicketTypeTeamAssignRules { get; set; } 


    }
}
