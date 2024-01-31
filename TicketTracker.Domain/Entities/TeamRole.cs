namespace TicketTracker.Domain.Entities
{
    public class TeamRole
    {
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public int TicketTypeConfigurationId { get; set; }

        //ef
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }
        public TeamRoleType? TeamRoleType { get; set; }
        public Team? Team {  get; set; }
    }
}