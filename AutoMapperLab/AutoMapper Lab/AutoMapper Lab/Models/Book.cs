using System;
using System.Collections.Generic;

namespace AutoMapper_Lab.Models
{
    public partial class Book
    {
        public Book()
        {
            Categories = new HashSet<Category>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int EditionType { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int AgeRestriction { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
    }
}
