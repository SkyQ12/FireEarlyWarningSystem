using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Context.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.UserName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.Property(x => x.UserPhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Sex);
            builder.Property(x => x.Birthday);
            builder.Property(x => x.HomeTown);
            builder.Property(x => x.Role).IsRequired();
            builder.HasMany(x => x.Cameras).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
