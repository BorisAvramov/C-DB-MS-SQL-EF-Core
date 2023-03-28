using AutoMapper_Lab.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper_Lab.Dtos;
using AutoMapper_Lab.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using AutoMapper_Lab.Profiles;

namespace AutoMapper_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;


            BookShopContext context = new BookShopContext();

            // before initial of mapper install NuGet package AUTOMAPPER


            var mapConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new BookProfile());

                //config.CreateMap<Book, BookInfoDto>()
                ////custom mapper for properties fullname of author
                //.ForMember(x => x.AuthorFullName, options => 
                //        options.MapFrom(x => $"{x.Author.FirstName} {x.Author.LastName}"))
                //// reverce map  map Book in db from BookInfoDto = reverce proccess 
                //.ReverseMap();
                //config.CreateMap<Category, CategoryInfoDto>()
                //.ReverseMap();
                

            });
            var mapper = mapConfiguration.CreateMapper();

            Book book = context.Books
                .Include(b => b.Author)
                .Include(b => b.Categories)
                .First();


            BookInfoDto bookDto = mapper.Map<BookInfoDto>(book);
            Console.WriteLine(bookDto.Title);
            Console.WriteLine(bookDto.AuthorFullName);
            Console.WriteLine(bookDto.AuthorFirstName);
            Console.WriteLine(bookDto.AuthorLastName);
            Console.WriteLine(String.Join(", ", bookDto.Categories.Select(c => c.Name)));
            Console.WriteLine(JsonConvert.SerializeObject(bookDto, Formatting.Indented));

            //auto mapping collection with .ProjecTo<dto>(mapConfig).ToList()

            //List<BookInfoDto> books = context.Books.Where(b => b.Title.StartsWith("A"))
            //    .ProjectTo<BookInfoDto>(mapConfiguration).ToList() ;

            //foreach (var curBook in books)
            //{
            //    Console.WriteLine(JsonConvert.SerializeObject(curBook, Formatting.Indented));


            //}

            

            Console.WriteLine("check");


        }
    }
}