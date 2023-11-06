using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XsisMovieAPI.Domain.Entities;

namespace XsisMovieAPI.Infrastructure.Persistance.Configurations {
    public class MovieConfiguration : IEntityTypeConfiguration<Movie> {
        public void Configure(EntityTypeBuilder<Movie> builder) {
            // Table Name
            builder.ToTable(nameof(Movie));

            builder.HasKey(x => x.Id);            
        }
    }
}
