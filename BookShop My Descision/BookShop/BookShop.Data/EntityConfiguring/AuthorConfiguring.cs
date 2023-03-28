using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.EntityConfiguring;

public class AuthorConfiguring : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
      

        
            builder
            .HasKey(e => e.AuthorId);
            builder.Property(e => e.FirstName)
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(e => e.LastName)
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired(true);


       

    }
}
