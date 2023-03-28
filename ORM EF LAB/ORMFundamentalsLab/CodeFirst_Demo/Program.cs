using CodeFirst_Demo.Models;

namespace CodeFirst_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // type in powershell: dotnet ef database update
            // or....   

            var db = new ApplicationDbContext();

            db.Database.EnsureCreated();


            //next time learn migration => update data definition like add colun etc.


            db.Categories.Add(new Category
            {
                Title = "Sport",
                News = new List<News>()
                {
                    new News {
                        Title = "CSKA bie Levski", 
                        Content = "ЦСКА бие Левски",
                        Comments = new List<Comment>()
                        {
                            new Comment {Authow = "Niki", Content = "Da"},
                            new Comment { Authow = "Stoyan", Content = "Ne"}
                        }
                    }
                }
            });

            db.SaveChanges();


        }
    }
}