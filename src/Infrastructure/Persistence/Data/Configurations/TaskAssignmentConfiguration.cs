using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> modelBuilder)
    {
        modelBuilder.HasKey(ta => new { ta.TaskId, ta.UserId });

        modelBuilder.Property(ta => ta.TaskId).IsRequired();
        
        modelBuilder.Property(ta => ta.UserId).IsRequired();
    }
}