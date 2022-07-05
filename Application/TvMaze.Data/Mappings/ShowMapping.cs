using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMaze.Domain.Entities;

namespace TvMaze.Data.Mappings;

public class ShowMapping : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasAlternateKey(s => s.Name);

        builder.HasMany(s => s.Cast)
            .WithOne(a => a.Show)
            .HasForeignKey(a => a.ShowId);

        builder.ToTable("Shows");
    }
}

