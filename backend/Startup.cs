//program.cs call to Startup
//here we define everything we need for the project

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.Business;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        readonly string corsSettings = "corsSettings";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsSettings,
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:4200",
                                "https://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("EventsDatabase")));
     
            // DI (Dependency Injection) framework Settings
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventTypeService, EventTypeService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISubRoleService, SubRoleService>();
            
            // Mapping Settings
            var mappingConfig = services.InitMappings();
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            

            //===== Identity User Configurations ====//
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // User settings
                    options.User.RequireUniqueEmail = true; // Make sure the email is unique for each account

                    // Password settings
                    options.Password.RequireDigit = false; // Do charge numbers
                    options.Password.RequiredLength = Constants.MIN_PASSWORD_LENGTH; // minimum length
                    options.Password.RequiredUniqueChars = 0; // how much special characters require
                    options.Password.RequireLowercase = false; // Do required lowercase
                    options.Password.RequireNonAlphanumeric = false; 
                    options.Password.RequireUppercase = false; // Do required uppercase

                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews(); // Angular
        
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/ClientApp/dist";
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

            app.UseCors(corsSettings);

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
