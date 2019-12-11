using Library.DataAccess.Interfaces;
using Library.DataAccess.Repository;
using Library.DataAccess.Repository.Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services,bool isDapperEnable)
        {
            if (!isDapperEnable)
            {
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped<IBookRepository, BookRepository>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IAuthorRepository, AuthorRepository>();
                services.AddScoped<IBooksAdnAuthorRepository, BooksAndAuthorRepository>();
            }
            if (isDapperEnable)
            {
                services.AddScoped(typeof(IRepository<>), typeof(RepositoryDapper<>));
                services.AddScoped<IBookRepository, BookRepositoryDapper>();
                services.AddScoped<IOrderRepository, OrderRepositoryDapper>();
                services.AddScoped<IAuthorRepository, AuthorRepositoryDapper>();
                services.AddScoped<IBooksAdnAuthorRepository, BooksAndAuthorRepository>();
            }
            
        }
    }
}
