using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketTracker.Infrastructure.DataBaseContext
{
    public class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
                

    }
}
