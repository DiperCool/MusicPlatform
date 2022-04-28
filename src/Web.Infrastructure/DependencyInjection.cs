using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Common.Interfaces;
using Web.Infrastructure.Identity;
using Web.Infrastructure.Persistence;
using Web.Infrastructure.Services;

namespace Web.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddIdentityServer()
            .AddSigningCredential(new X509Certificate2(@"server.pfx", "09210921"))
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        services.AddAuthentication()
            .AddIdentityServerJwt();
        services.AddAuthorization();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IProfileService, ProfileService>();
        services.AddTransient<IFileService, FileService>();
        return services;
    }
}