using BookShop.Data.EntityConfiguring;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext()
        {

        }

        public BookShopContext(DbContextOptions options)
            :base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category>Categories { get; set; }

        public DbSet<BookCategory> BooksCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Author>(new AuthorConfiguring());

            modelBuilder.ApplyConfiguration<Book>(new BookConfiguring());

            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguring());

            modelBuilder.ApplyConfiguration<BookCategory>(new BookCategoryConfiguring());



        }


    }
}