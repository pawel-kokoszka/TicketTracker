namespace TicketTracker.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }  
        public int  TypeId { get; set; }
        public TicketType? TicketType { get; set; } //= new();
        
        public int TicketTypeConfigurationId { get; set; }
        

        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }

        public string? CreatedByUserId { get; set; }

        
        public string? EditedByUserId { get; set; }
        public ApplicationUser? EditorUser { get; set; }
        
        
        
        
        public int AssigningTeamId { get; set; }
        public Team? AssigningTeam { get; set; }


        public ApplicationUser? CreatorUser { get; set; }


        public string? AssignedUserId { get; set; }
        public ApplicationUser? AssignedUser { get; set; }

        public int? AssignedTeamId { get; set; } 
        public Team? AssignedTeam { get; set; }

        public bool IsDeleted { get; set; }
        
        public string? Description { get; set; } 
        public string? ShortDescription { get; set; }

        public int ProjectConfigurationId { get; set; }
        public ProjectConfiguration? ProjectConfiguration { get; set; } //= new();

        public int TicketSlaConfigurationId { get; set; }
        public TicketSlaConfiguration? TicketSlaConfigurations { get; set; }
               

        public int TicketStatusId { get; set; }              
        public TicketStatus? TicketStatuses { get; set; }

        public int TicketServiceId { get; set; }
        public TicketService? TicketService { get; set; }

        public int TicketSubServiceId { get; set; }
        public TicketSubService? TicketSubService { get; set; }

        

        



        public List<Comment>? Comments { get; set; } //= new();
    }
}

