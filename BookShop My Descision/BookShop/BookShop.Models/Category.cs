using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Category
    {
        public Category()
        {
            this.CategoryBooks = new HashSet<BookCategory>();
        }

        public int CategoryId { get; set; }
        public string Name  { get; set; }

        public ICollection<BookCategory> CategoryBooks { get; set; }

    }
}
