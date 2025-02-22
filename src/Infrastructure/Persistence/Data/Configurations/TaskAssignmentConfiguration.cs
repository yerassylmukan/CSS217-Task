using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> modelBuilder)
    {
        modelBuilder.HasKey(ta => new { ta.TaskId, ta.UserId });
        
        modelBuilder.
            HasOne(ta => ta.Task)
            .WithMany(t => t.Assignments)
            .HasForeignKey(ta => ta.TaskId);

        modelBuilder.Property(ta => ta.UserId).IsRequired();
    }
}