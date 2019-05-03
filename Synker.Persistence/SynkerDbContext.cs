namespace Synker.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SynkerDbContext : DbContext, ISynkerDbContext
    {
        public SynkerDbContext(DbContextOptions<SynkerDbContext> options)
           : base(options)
        {
        }

        public DbSet<PlaylistDataSource> PlaylistDataSources { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SynkerDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            UpdateUpdatedProperty();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateUpdatedProperty()
        {
            foreach (var entry in ChangeTracker
                .Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) && e.Entity is EntityAudit))
            {
                var entity = entry.Entity as EntityAudit;

                entity.UpdatedDate = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
