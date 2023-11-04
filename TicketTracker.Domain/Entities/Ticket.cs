﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class Ticket
    {
        public required int Id { get; set; }    

        public int  TypeId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int EnvId { get; set; }

        public int StatusId { get; set; }

        public int AssignedTo { get; set; }

        public int CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int PriorityId { get; set; }

        public string? Description { get; set; } 


    }
}

