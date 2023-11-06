using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using XsisMovieAPI.Application.Interfaces;
using XsisMovieAPI.Domain.Common;
using XsisMovieAPI.Domain.Entities;
using XsisMovieAPI.Infrastructure.Persistance.Configurations;

namespace XsisMovieAPI.Infrastructure.Persistance {
    public class AppDbContext : DbContext {
        private readonly IDateTime _dateTime;
        protected readonly IConfiguration _Configuration;
        public AppDbContext(DbContextOptions options, IDateTime dateTime, IConfiguration configuration) : base(options) {
            _dateTime = dateTime;
            _Configuration = configuration;
        }

        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(_Configuration.GetConnectionString("postgresdb"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            foreach (EntityEntry<AuditEntity> entry in ChangeTracker.Entries<AuditEntity>()) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.Now;
                        if (string.IsNullOrWhiteSpace(entry.Entity.CreatedBy)) {
                            entry.Entity.CreatedBy = "System";
                        }
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = _dateTime.Now;
                        if (string.IsNullOrWhiteSpace(entry.Entity.UpdatedBy)) {
                            entry.Entity.UpdatedBy = "System";
                        }
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
