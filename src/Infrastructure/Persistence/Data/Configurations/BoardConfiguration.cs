using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> modelBuilder)
    {
        modelBuilder.HasKey(b => b.Id);

        modelBuilder.Property(b => b.Id).ValueGeneratedOnAdd();

        modelBuilder
            .HasMany(b => b.Columns)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId);
    }
}