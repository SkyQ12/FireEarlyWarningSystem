using Microsoft.EntityFrameworkCore;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Context.Configurations
{
    public class WarningHistoryEntityTypeConfiguration : IEntityTypeConfiguration<WarningHistory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WarningHistory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.CameraId);

            builder.Property(e => e.TemperatureValue);

            builder.Property(e => e.SmokeValue);

            builder.Property(e => e.YoloDetection);

            builder.Property(e => e.CameraState);

            builder.Property(e => e.WarningTime);

            builder.HasIndex(e => e.CameraId);
            builder.HasOne(e => e.Camera).WithMany(c => c.WarningHistories).HasForeignKey(e => e.CameraId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
