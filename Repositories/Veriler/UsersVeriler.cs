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
    public class UsersVeriler : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
           
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Name);
            builder.Property(u => u.SurName);
            builder.Property(u => u.Email);
            builder.Property(u => u.Password);
            builder.Property(u => u.UserName);
            builder.Property<int>(u => u.UserId);
            builder.HasData(
                new Users() { UserId = 1, Name = "Baran Uğur", SurName = "Özdemir", Email = "ozdemirbaranugur@example.com", Password = "baranugurozdemir", UserName = "ozdemir" ,UserRoleId=2},
                new Users() { UserId = 2, Name = "Yusuf", SurName = "Tunç", Email = "tuncyusuf@example.com", Password = "yusuftunc", UserName = "tunc" ,UserRoleId=2},
                new Users() { UserId = 3, Name = "Ömer", SurName = "Soydinç", Email = "soydincomer@example.com", Password = "omersoydınc", UserName = "soydınc" ,UserRoleId=2},
                new Users() { UserId = 4, Name = "Zeynep", SurName = "Kaya", Email = "zeynep.kaya@example.com", Password = "Password101", UserName = "zeynep" ,UserRoleId=1}

                );
        }
    }
}
