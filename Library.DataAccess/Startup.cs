using Library.DataAccess.Interfaces;
using Library.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

/*            services.AddScoped<IUserRepository, UserRepository>();
*/            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBooksAdnAuthorRepository, BooksAndAuthorRepository>();

        }
    }
}
