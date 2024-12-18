﻿using Domain.Identity.Entities;
using Infastructure.Identity.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Identity
{
    public static class IdentityServiceDependentInjection
    {

        public static void AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataBaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MyCA-Identity"),
                    b => b.MigrationsAssembly(typeof(IdentityDataBaseContext).Assembly.FullName)));
        }
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            //https://devblogs.microsoft.com/dotnet/whats-new-with-identity-in-dotnet-8/
            //https://medium.com/@xsoheilalizadeh/asp-net-core-identity-deep-dive-stores-e0e54291b51d
            //services.AddIdentityCore<IdentityUser>()
            //    .AddEntityFrameworkStores<IdentityDataBaseContext>();
            services.AddDefaultIdentity<ApplicationUser>()  //Remember to Add Microsoft.AspNetCore.Identity.UI
                .AddEntityFrameworkStores<IdentityDataBaseContext>();
//    .AddEntityFrameworkStores<ApplicationDbContext>();
            //.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true);
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 7;
            });
            

        }

    }
}
