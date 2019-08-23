using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using papuff.domain.Core.Base;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace papuff.datainfra.ORM {
    public class AppDbContext : DbContext {

        #region - dbset -

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<General> Generals { get; set; }
        public DbSet<Siege> Sieges { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Company> Companies { get; set; }

        //public DbSet<Advertising> Advertisings { get; set; }
        //public DbSet<Favorite> Favorites { get; set; }

        //public DbSet<LogApp> LogApp { get; set; }
        //public DbSet<LogCore> LogCore { get; set; }
        //public DbSet<LogKernel> LogKernel { get; set; }

        #endregion

        public AppDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder options) {
            #region - scheme -

            options.HasDefaultSchema("Core");

            // # config conventions by metadata model loop
            foreach (var entityType in options.Model.GetEntityTypes()) {
                // # config equivalent
                // # convention Remove Pluralizing Table Names Covention
                entityType.Relational().TableName = entityType.DisplayName();

                // # config equivalent
                // # convention Remove OneToMany Cascade Delete Convention
                // # convention Remove ManyToMany Cascade Delete Convention
                entityType.GetForeignKeys()
                    .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
            }

            #endregion

            #region - mappings -

            // # log
            //options.Entity<EntityLog>(new EntityLogMap().Configure);

            #endregion

            base.OnModelCreating(options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            // # enable lazy loading be careful
            //options.UseLazyLoadingProxies();
            //options.EnableSensitiveDataLogging();

            base.OnConfiguring(options);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) {
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
            entry.Entity.GetType().GetProperty(nameof(EntityBase.CreatedAt)) != null ||
            entry.Entity.GetType().GetProperty(nameof(EntityBase.UpdatedAt)) != null ||
            entry.Entity.GetType().GetProperty(nameof(EntityBase.Id)) != null)) {
                if (entry.Property(nameof(EntityBase.CreatedAt)) != null)
                    if (entry.State == EntityState.Added)
                        entry.Property(nameof(EntityBase.CreatedAt)).CurrentValue = DateTimeOffset.UtcNow;
                    else if (entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.CreatedAt)).IsModified = false;

                if (entry.Property(nameof(EntityBase.UpdatedAt)) != null)
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                        entry.Property(nameof(EntityBase.UpdatedAt)).CurrentValue = DateTimeOffset.UtcNow;

                if (entry.Property(nameof(EntityBase.Id)) != null)
                    if (entry.State == EntityState.Added)
                        if (string.IsNullOrEmpty((string)entry.Property(nameof(EntityBase.Id)).CurrentValue))
                            entry.Property(nameof(EntityBase.Id)).CurrentValue = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
