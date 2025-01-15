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
    public class MakaleCommentVeriler : IEntityTypeConfiguration<MakaleComment>
    {
        public void Configure(EntityTypeBuilder<MakaleComment> builder)
        {
            builder.HasKey(mc => mc.MakaleCommentId);

            builder.Property(mc => mc.Comment)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(mc => mc.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(mc => mc.IsActive)
                .HasDefaultValue(true);

            // Seed data
            builder.HasData(
                new MakaleComment
                {
                    MakaleCommentId = 1,
                    MakaleId = 1,
                    UserId = 1,
                    Comment = "baran emmi",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                }
            );
        }
    }
}
