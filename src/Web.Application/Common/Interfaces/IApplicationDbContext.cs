using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;

namespace Web.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get;set; }
    DbSet<Artist> Artists { get; set; }
    DbSet<Listener> Listeners { get; set; }
    DbSet<Profile> Profiles { get; set; }
    DbSet<Song> Songs { get; set; }
    DbSet<Album> Albums { get; set; }
    DbSet<PathToFile> PathesToFiles { get; set; }
    DbSet<Like> Likes { get; set; }
     DbSet<Subscriber> Subscribers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}