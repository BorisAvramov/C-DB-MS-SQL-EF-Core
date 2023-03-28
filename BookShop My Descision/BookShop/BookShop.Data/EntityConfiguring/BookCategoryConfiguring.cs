using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.EntityConfiguring
{
    public class BookCategoryConfiguring : IEntityTypeConfiguration<BookCategory>
    {
      

        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(e => new { e.BookId, e.CategoryId });

            builder.HasOne(e => e.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(e => e.BookId);
            builder.HasOne(e => e.Category)
                .WithMany(c => c.CategoryBooks)
                .HasForeignKey(e => e.CategoryId);

        }
    }
}
