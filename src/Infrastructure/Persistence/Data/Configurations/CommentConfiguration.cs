using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> modelBuilder)
    {
        modelBuilder.HasKey(c => c.Id);

        modelBuilder.Property(c => c.Id).ValueGeneratedOnAdd();

        modelBuilder.Property(c => c.Content).IsRequired();

        modelBuilder.Property(c => c.UserId).IsRequired();

        modelBuilder.Property(c => c.TaskId).IsRequired();
    }
}