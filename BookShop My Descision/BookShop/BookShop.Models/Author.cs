﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Author
    {
        public Author()
        {
            this.Books= new HashSet<Book>();
        }
        
        public int AuthorId { get; set; }

        public string FirstName  { get; set; }
        public string LastName  { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}