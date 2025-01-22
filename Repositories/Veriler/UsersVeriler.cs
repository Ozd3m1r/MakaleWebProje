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
                new Users() { UserId = 1, Name = "Baran Uğur", SurName = "Özdemir", Email = "ozdemirbaranugur@example.com", Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserName = "ozdemir" ,UserRoleId=2},
                new Users() { UserId = 2, Name = "Yusuf", SurName = "Tunç", Email = "tuncyusuf@example.com", Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserName = "tunc" ,UserRoleId=2},
                new Users() { UserId = 3, Name = "Ömer", SurName = "Soydinç", Email = "soydincomer@example.com", Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserName = "soydınc" ,UserRoleId=2},
                new Users() { UserId = 4, Name = "Ali", SurName = "Arı", Email = "aliari@example.com", Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserName = "ari" ,UserRoleId=1}

                );
        }
    }
}
