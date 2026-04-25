using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Context.Configurations
{
    public class CameraEntityTypeConfiguration : IEntityTypeConfiguration<Camera>
    {
        public void Configure(EntityTypeBuilder<Camera> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(20);
            builder.Property(x => x.CameraName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Battery);
            builder.Property(x => x.FireSmokeImage);
            builder.Property(x => x.YoloDetection);
            builder.Property(x => x.TemperatureValue);
            builder.Property(x => x.SmokeValue);
            builder.Property(x => x.CameraState);
            builder.Property(x => x.TimeStamp);
            builder.Property(x => x.RegistationDate);
            builder.HasMany(x => x.WarningHistories).WithOne(x => x.Camera).HasForeignKey(x => x.CameraId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
