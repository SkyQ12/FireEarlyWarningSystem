using FireEarlyWarningSystem.Infrastructure.Domain.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Camera> Cameras { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.WarningHistory> WarningHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CameraEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarningHistoryEntityTypeConfiguration());
        }
    }
}
