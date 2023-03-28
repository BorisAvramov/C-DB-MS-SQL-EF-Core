using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.EntityConfiguring;

public class BookConfiguring : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.BookId);

        builder.Property(e => e.Title)
            .IsUnicode(true)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(e => e.Description)
           .IsUnicode(true)
           .IsRequired(true)
           .HasMaxLength(1000);

        builder.Property(e => e.ReleaseDate)
         .IsRequired(false);

        builder.HasOne(e => e.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(e => e.AuthorId);

        

        
    }
}
