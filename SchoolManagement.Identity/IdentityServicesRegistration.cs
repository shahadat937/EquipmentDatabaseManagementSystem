using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Models.Identity;
using SchoolManagement.Identity.Models;
using SchoolManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SchoolManagement.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<SchoolManagementIdentityDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("SchoolManagementIdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(SchoolManagementIdentityDbContext).Assembly.FullName)));

            //int year = DateTime.Now.Year;
            //DateTime day = new DateTime(year, 1, 1).AddMonths(10);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SchoolManagementIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();

            //if (DateTime.Now.Date > day.Date) return services;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });

            return services;
        }
    }
}
