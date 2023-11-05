using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext
{
    public class TrackerDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

    }
}
