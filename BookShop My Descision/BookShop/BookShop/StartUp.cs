using BookShop.Data;
using BookShop.Initializer;

namespace BookShop;

public class StartUp
{
    static void Main(string[] args)
    {
       using BookShopContext context = new BookShopContext();

        DbInitializer.ResetDatabase(context);

    }
}