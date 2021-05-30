using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.WebApplication.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Stripe;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Library.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            //var elasticUri = Configuration["ESUri"];

            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.WithExceptionDetails()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
            //    {
            //        AutoRegisterTemplate = true,
            //    })
            //.CreateLogger();
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<SmtpOptions>(Configuration.GetSection("SmtpOptions"));
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
            //services.Configure<ElasticsearchOption>(Configuration.GetSection("ESUri"));

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(policyBuilder => policyBuilder
            //        .AllowAnyOrigin()
            //        .AllowAnyHeader()
            //        .AllowAnyMethod());
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksShop-API", Version = "v1" });
            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            BusinessLogic.Startup.ConfigureServices(services, connectionString, isDapperEnable: true);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                 
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration.GetSection("JwtOptions")["JwtIssuer"],
                        ValidAudience = Configuration.GetSection("JwtOptions")["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtOptions")["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("foo");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksMagazine-API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

