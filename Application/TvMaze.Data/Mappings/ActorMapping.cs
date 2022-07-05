
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMaze.Domain.Entities;

namespace TvMaze.Data.Mappings;

public class ActorMapping : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.Name);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(a => a.BirthDay)
            .IsRequired();

        builder.HasOne(a => a.Show)
            .WithMany(s => s.Cast);

        builder.ToTable("Actors");
    }
}

