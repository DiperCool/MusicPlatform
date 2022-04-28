using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.Entities;
using Web.Infrastructure.Identity;
namespace Web.Infrastructure.Persistence;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<Account> Accounts { get;set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Listener> Listeners { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<PathToFile> PathesToFiles { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }

    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}