using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Library.BusinessLogic.Configuration;
using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Interfaces;
using Library.DataAccess.Repository;
using Library.BusinessLogic.Services;
using Library.DataAccess;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Library.BusinessLogic
{

    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
           
            services.AddScoped<IDbConnection, SqlConnection>(x => new SqlConnection(connectionString));
          
            services.AddDbContext<MagazineContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Library.WebApplication")));
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBooksAndAuthorService, BooksAndAuthorService>();
            services.AddScoped<IUserService, UserService>();

            DataAccess.Startup.ConfigureServices(services);
            ConfigureAutomapper(services);
        }

        public static void ConfigureAutomapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BookProfile());
                mc.AddProfile(new AuthorProfile());
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


        }

    }

}
