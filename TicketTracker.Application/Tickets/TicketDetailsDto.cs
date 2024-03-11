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



        public int AssigningTeamId { get; set; }

        public string? AssigningTeamName { get; set; }


        public string? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }


        public int? AssignedTeamId { get; set; }
        public string? AssignedTeamName { get; set; }


        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        
        public string? ProjectName { get; set; }
        public int ProjectConfigurationId { get; set; }
        
        public int TicketSlaConfigurationId { get; set; }
        public string? TicketSlaConfigurationName { get; set; }
        public string? TicketSlaTimeLeft { get; set; }
        public string? TicketSlaResolutionDate { get; set; }

        public bool IsOverdue { get; set; }

        

        public int TicketStatusId { get; set; }
        public string? TicketStatusName { get; set; }

        public int TicketServiceId { get; set; }
        public string? TicketServiceName { get; set; }

        public int TicketSubServiceId { get; set; }
        public string? TicketSubServiceName { get; set; }



        public string? EnvironmentType { get; set; }
        public string? EnvironmentName { get; set; }

        public bool IsEditable { get; set; } //user can access Edit View and change all or specific tt properties
        public bool IsAbleToComment { get; set; } 

        
        public bool IsUserInCreatorTeam { get; set; }
        public bool IsUserInAssignedTeam {  get; set; } 
        public bool IsUserInEditOnlyTeam {  get; set; } //user can only edit tt properties unrelated to tt status


        //public List<Comment>? Comments { get; set; }

    }
}
