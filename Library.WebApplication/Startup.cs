using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.WebApplication.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Library.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy",
                     b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials().WithExposedHeaders("Token-Expired"));
                options.AddPolicy("OriginPolicy",
                  b => b.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksShop-API", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");/*
            bool isDapperEnable = bool.Parse(Configuration["RepositoryPattern"].Value);*/
            bool isDapperEnable = bool.Parse(Configuration.GetSection("RepositoryPattern")["isDapper"]);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            BusinessLogic.Startup.ConfigureServices(services, connectionString, isDapperEnable);

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksMagazine-API");
                    c.RoutePrefix = string.Empty;
                });
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAllPolicy");
            app.UseMvc();
        }
    }
}
