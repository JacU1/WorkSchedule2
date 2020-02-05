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
        /*
        IUserRepository UserRepository = new MockUserRepository();
        IDealRepository DealRepository = new MockDealRepository();
        IShiftRepository ShiftRepository = new MockShiftRepository();
        ISummaryRepository SummaryRepository = new MockSummaryRepository();
        */
        public WorkScheduleContext(DbContextOptions<WorkScheduleContext> options)
                : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deal>().HasData(
                    DealRepository.AllDeals
                );
            modelBuilder.Entity<User>().HasData(
                     UserRepository.AllUsers
                );
            modelBuilder.Entity<Shift>().HasData(
                    ShiftRepository.AllShifts
                );
            modelBuilder.Entity<Summary>().HasData(
                    SummaryRepository.AllSummary
                );

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkScheduleDB;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        */
    }
}
