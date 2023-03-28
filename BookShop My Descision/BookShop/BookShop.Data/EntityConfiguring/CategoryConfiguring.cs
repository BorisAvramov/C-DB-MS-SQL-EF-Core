using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Data.EntityConfiguring;

public class CategoryConfiguring : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        builder.HasKey(e => e.CategoryId);

        builder.Property(e => e.Name)
            .IsRequired(true)
            .IsUnicode(true)
            .HasMaxLength(50);

    }
}
