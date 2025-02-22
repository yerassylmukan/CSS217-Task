using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> modelBuilder)
    {
        modelBuilder.HasKey(c => c.Id);

        modelBuilder.Property(c => c.Id).ValueGeneratedOnAdd();

        modelBuilder
            .HasMany(c => c.Tasks)
            .WithOne(t => t.Column)
            .HasForeignKey(t => t.ColumnId);
    }
}