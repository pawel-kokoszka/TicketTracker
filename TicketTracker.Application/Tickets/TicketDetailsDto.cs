namespace TicketTracker.Application.Tickets
{
    public class TicketDetailsDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public string? TicketTypeName { get; set; }
        public int TicketTypeConfigurationId { get; set; }

        public string? DateCreated { get; set; } 
        public string? DateEdited { get; set; } 
        
        public string? CreatedByUserId { get; set; }
        public string? CreatedByUserName { get; set; }

        public string? EditedByUserId { get; set; }
        public string? EditedByUserName { get; set; }

        public string? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }
        
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
               

        public string? ProjectName { get; set; }
        public int ProjectConfigurationId { get; set; }
        
        public int TicketSlaConfigurationId { get; set; }
        public string? TicketSlaConfigurationName { get; set; }   
        
        
        
        
        public string? EnvironmentType { get; set; }
        public string? EnvironmentName { get; set; }



        //public List<Comment>? Comments { get; set; }

    }
}
