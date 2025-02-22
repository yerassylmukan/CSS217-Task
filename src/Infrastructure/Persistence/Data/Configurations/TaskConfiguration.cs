using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Task;

namespace Persistence.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> modelBuilder)
    {
        modelBuilder.HasKey(t => t.Id);

        modelBuilder.Property(t => t.Title).IsRequired();
    }
}