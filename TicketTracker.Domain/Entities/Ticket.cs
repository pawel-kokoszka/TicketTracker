﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }  
        public int  TypeId { get; set; }
        public TicketType? TicketType { get; set; } //= new();


        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }




        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatorUser { get; set; }

        public string? EditedByUserId { get; set; }
        public ApplicationUser? EditorUser { get; set; }

        public string? AssignedToUserId { get; set; }       
        public ApplicationUser? AssignedUser { get; set; }

        
        public bool IsDeleted { get; set; }
        
        
        
        
      

        public string? Description { get; set; } 
        public string? ShortDescription { get; set; }


        public List<Comment>? Comments { get; set; } //= new();

        public int TicketSlaId { get; set; }

        public TicketSla? TicketSla { get; set; }
        public int TicketStateId { get; set; }
        public int TicketServiceId { get; set; }
        public int ProjectConfigurationId { get; set; }
        public ProjectConfiguration? ProjectConfiguration { get; set; } //= new();

    }
}

