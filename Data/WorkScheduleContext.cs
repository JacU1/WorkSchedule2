using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkSchedule2.Models;

namespace WorkSchedule2.Data
{
    public class WorkScheduleContext : DbContext
    {
        public WorkScheduleContext(DbContextOptions<WorkScheduleContext> options)
                : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Sugestion> Sugestions { get; set; }
       
    }
}
