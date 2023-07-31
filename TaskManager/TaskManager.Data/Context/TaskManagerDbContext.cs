using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;

namespace TaskManager.Data.Context
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
    }
}
