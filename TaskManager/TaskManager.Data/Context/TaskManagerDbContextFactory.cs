using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.Data.Context
{
    public class TaskManagerDbContextFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
    {
        public TaskManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=WMC-PC\\SQLEXPRESS;Initial Catalog=DB_AprendizadoTS;User ID=sa;Password=admin123;Encrypt=False");

            return new TaskManagerDbContext(optionsBuilder.Options);
        }
    }
}
