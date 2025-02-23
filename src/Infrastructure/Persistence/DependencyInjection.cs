using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static void AddPersistenceServiceCollection(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        service.AddScoped<IBoardRepository, BoardRepository>();
        service.AddScoped<IColumnRepository, ColumnRepository>();
        service.AddScoped<ICommentRepository, CommentRepository>();
        service.AddScoped<ITaskAssignmentRepository, TaskAssignmentRepository>();
        service.AddScoped<ITaskRepository, TaskRepository>();
    }
}