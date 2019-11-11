using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Library.BusinessLogic.Configuration;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services;
using Library.DataAccess;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Library.BusinessLogic.Helper;

namespace Library.BusinessLogic
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {           
            services.AddScoped<IDbConnection, SqlConnection>(x => new SqlConnection(connectionString));
          
            services.AddDbContext<MagazineContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Library.DataAccess")));
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBooksAndAuthorService, BooksAndAuthorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStripeService, StripeService>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MagazineContext>()
                .AddDefaultTokenProviders();

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
                mc.AddProfile(new OrderProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
