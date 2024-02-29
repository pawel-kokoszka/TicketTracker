namespace TicketTracker.Domain.Entities
{
    public class TicketFlowConfiguration
    {
        public int Id { get; set; }
        public int TicketTypeConfigurationId { get; set; }
        public int StatusId { get; set; }
        public int NextStatusId { get; set; }

        public bool IsNewTicketFirstStatus { get; set; } 

        public bool CreatorCanChangeStatus { get; set; }
        public bool AssignedUserCanChangeStatus { get; set; }


        //EF
        public TicketStatus? TicketStatuses { get; set; }
        public TicketTypeConfiguration? TicketTypeConfigurations { get; set; }
    }
}
