using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper_Lab.Dtos
{
    internal class BookInfoDto
    {


        //public int BookId { get; set; }


        public string Title { get; set; }

        public decimal Price { get; set; }

        public string AuthorFullName { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public ICollection<CategoryInfoDto> Categories { get; set; }

    }
}
