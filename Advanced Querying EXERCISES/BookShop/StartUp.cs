namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);


            //02. Age Restriction
            //string command = Console.ReadLine();
            //Console.WriteLine(GetBooksByAgeRestriction(db, command));

            //03. Golden Books

            //Console.WriteLine(GetGoldenBooks(db));

            //04. Books by Price

            //Console.WriteLine(GetBooksByPrice(db));

            //05. Not Released In
            //int year = int.Parse(Console.ReadLine());

            //Console.WriteLine(GetBooksNotReleasedIn(db, year));

            //06. Book Titles by Category

            //string input =  Console.ReadLine();

            //Console.WriteLine(GetBooksByCategory(db, input));

            // 07. Released Before Date

            //string date = Console.ReadLine();

            //Console.WriteLine(GetBooksReleasedBefore(db, date));

            //08. Author Search

            //string input = Console.ReadLine();
            //Console.WriteLine(GetAuthorNamesEndingIn(db, input));


            // 09. Book Search

            //string input = Console.ReadLine();
            //Console.WriteLine(GetBookTitlesContaining(db, input));

            //10. Book Search by Author





        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {

        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            string[] titles = context.Books
                .OrderBy(b => b.Title)
                .Where(b => b.Title.ToLower().IndexOf(input.ToLower()) != -1)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, titles);


        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {

            var authorNames = context.Authors
                    
                    .Where(a => a.FirstName.EndsWith(input))
                    .OrderBy(a => a.FirstName)
                    .ThenBy(a => a.LastName)
                    .Select(a => new { FullName = $"{a.FirstName} {a.LastName}" })
                    
                    .ToList();

            return String.Join(Environment.NewLine, authorNames.Select(a => a.FullName));



        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateParced = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Date < dateParced.Date)
                .Select(b => new
                {
                    ReleaseDate = b.ReleaseDate,
                    Title = b.Title,
                    EditipnType = b.EditionType.ToString(),
                    Price = b.Price.ToString("f2")
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditipnType} - ${b.Price}"));

        }


        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string>categories = input.Split().Select(c => c.ToLower()).ToList();

            var booksTitles = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => new
                {
                    Title = b.Title

                })
                .OrderBy(b => b.Title)
                .ToList();

            return String.Join(Environment.NewLine, booksTitles.Select(t => t.Title));







        }


        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => new { Title = b.Title})
                .ToArray();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));


        }



        public static string GetBooksByPrice(BookShopContext context)
        {

            var books = context.Books
                .Where(b => b.Price > 40)
                .ToArray()
                .Select(b => new
                {
                    b.Title,
                    Price = b.Price.ToString("f2")
                })
                .OrderByDescending(b => b.Price)
                .ToArray();

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price}"));


        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooksTitles = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, goldenBooksTitles);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            try
            {
                AgeRestriction commandParcedToEnum = Enum.Parse<AgeRestriction>(command,true);
                var bookTitles = context.Books

               .Where(b => b.AgeRestriction  == commandParcedToEnum)
               .OrderBy(b => b.Title)
               .Select(b => b.Title)
               .ToArray();


                return String.Join(Environment.NewLine, bookTitles);
            }
            catch (Exception e)
            {
                return e.Message;
            }







        }
    }
}


