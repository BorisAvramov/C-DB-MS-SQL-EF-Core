using System;
using System.Collections.Generic;
using AutoMapper_Lab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoMapper_Lab.Data
{
    public partial class BookShopContext : DbContext
    {
        public BookShopContext()
        {
        }

        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-6ADIQGK\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.AuthorId, "IX_Books_AuthorId");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId);

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BooksCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                        j =>
                        {
                            j.HasKey("BookId", "CategoryId");

                            j.ToTable("BooksCategories");

                            j.HasIndex(new[] { "CategoryId" }, "IX_BooksCategories_CategoryId");
                        });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
