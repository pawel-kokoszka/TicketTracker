using Microsoft.AspNetCore.Identity;
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
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }

        public int EnvId { get; set; }

        public int StatusId { get; set; }

        public int AssignedTo { get; set; }

        public string? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; }     
        public string? EditedByUserId { get; set; }
        public string? EditedByUserName { get; set; }
        public bool IsDeleted { get; set; }
        public int PriorityId { get; set; }
        public string? Description { get; set; } 
        public string? ShortDescription { get; set; }

        public List<Comment> Comments { get; set; } = new(); 
        //to check if it is running with every record in index View - where is not needed 

        
    }
}

