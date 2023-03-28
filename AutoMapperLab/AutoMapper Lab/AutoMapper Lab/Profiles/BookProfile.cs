using AutoMapper;
using AutoMapper_Lab.Dtos;
using AutoMapper_Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper_Lab.Profiles
{
    public class BookProfile : Profile
    {
        public  BookProfile()
        {
            CreateMap<Book, BookInfoDto>()
                //custom mapper for properties fullname of author
                .ForMember(x => x.AuthorFullName, options =>
                        options.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"))
                // reverce map  map Book in db from BookInfoDto = reverce proccess 
                .ReverseMap();
            CreateMap<Category, CategoryInfoDto>()
                .ReverseMap();


        }
    }
}
