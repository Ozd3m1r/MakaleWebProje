using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Veriler
{
    public class UserRoleVeriler:IEntityTypeConfiguration<UserRole>
    {
        

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.UserRoleId);
            builder.Property(ur=>ur.UserRoleName);
            builder.HasData(
                new UserRole() { UserRoleId = 1, UserRoleName = "User" },
                new UserRole() { UserRoleId = 2, UserRoleName = "Admin" }
                );
        }
    }
}
