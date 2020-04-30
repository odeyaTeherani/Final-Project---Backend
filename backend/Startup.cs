//program.cs call to Startup
//here we define everything we need for the project

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace backend
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
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("EventsDatabase")));
     
            // DI (Dependency Injection) framework Settings
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventTypeService, EventTypeService>();
            services.AddTransient<IAccountService, AccountService>();

            // Mapping Settings
            var mappingConfig = services.InitMappings();
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            // services.AddDefaultIdentity<ApplicationUser>(options =>
            //         options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            //===== Identity User Configurations ====//
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // User settings
                    options.User.RequireUniqueEmail = true; // Make sure the email is unique for each account

                    // Password settings
                    options.Password.RequireDigit = false; // Do charge numbers
                    options.Password.RequiredLength = 6; // minimum length
                    options.Password.RequiredUniqueChars = 0; // how much special characters require
                    options.Password.RequireLowercase = false; // Do required lowercase
                    options.Password.RequireNonAlphanumeric = false; 
                    options.Password.RequireUppercase = false; // Do required uppercase

                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // services.AddIdentityServer()
            //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services.AddAuthentication()
            //     .AddIdentityServerJwt();

            services.AddControllersWithViews(); // Angular
            // services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // ===== Add Jwt Authentication ======== //
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
                    cfg.TokenValidationParameters = new TokenValidationParameters // don't required
                    {
                        ValidIssuer = Configuration["JwtIssuer"], // token issuer name
                        ValidAudience = Configuration["JwtIssuer"], 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])), // Symmetric key - Extra security on the token
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = "roles", // name of roles claim 
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAccountService accountService)
        {
            accountService.SeedRoles();
            app.UseExceptionHandler(env.IsDevelopment() ? "/error-local-development" : "/error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            // app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller}/{action=Index}/{id?}");a
                // endpoints.MapRazorPages(); 
                
                endpoints.MapControllers();
                // endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
