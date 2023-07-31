using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Data.Context;

namespace TaskManager.Data.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskManagerDbContext _dbContext; 

        public ProjectTaskRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectTask> GetTaskAsync(int id)
        {
            return await _dbContext.ProjectTasks.FindAsync(id);
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await _dbContext.ProjectTasks.ToListAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _dbContext.ProjectTasks.FindAsync(id);
            _dbContext.ProjectTasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProjectTask> AddTaskAsync(ProjectTask projectTask)
        {
            using (SqlConnection connection = new SqlConnection)

            return projectTask;
        }
    }
}
