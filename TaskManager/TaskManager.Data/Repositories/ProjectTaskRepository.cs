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
            using (SqlConnection connection = new("Data Source = DESKTOP - THJUGI2\\SQLEXPRESS; Initial Catalog = DB_AprendizadoTS; User ID = sa; Password = admin123; Encrypt = False"))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new(
                    "INSERT INTO " +
                    "ProjectTasks (Title, Description, IsComplete, CreateDate, CompletedDate) " +
                    "VALUES (@title, @description, @isComplete, @createDate, @completedDate); " +
                    "SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@title", projectTask.Title);
                    command.Parameters.AddWithValue("@description", projectTask.Description);
                    command.Parameters.AddWithValue("@isComplete", projectTask.IsComplete);
                    command.Parameters.AddWithValue("@createDate", projectTask.CreateDate);
                    command.Parameters.AddWithValue("@completedDate", projectTask.CompletedDate);

                    // Execute the command and get the ID of the newly created task
                    var result = await command.ExecuteScalarAsync();
                    projectTask.Id = int.Parse(result.ToString());
                }
            }

            return projectTask;
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask projectTask)
        {
            using (SqlConnection connection = new("Data Source = DESKTOP - THJUGI2\\SQLEXPRESS; Initial Catalog = DB_AprendizadoTS; User ID = sa; Password = admin123; Encrypt = False")) 
            {
                await connection.OpenAsync();

                using (SqlCommand command = new(
                    "UPDATE ProjectTasks " +
                    "SET Title = @title" +
                    ", Description = @description" +
                    ", IsComplete = @isComplete" +
                    ", CreateDate = @createDate" +
                    ", CompletedDate = @completedDate " +
                    "WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@title", projectTask.Title);
                    command.Parameters.AddWithValue("@description", projectTask.Description);
                    command.Parameters.AddWithValue("@isComplete", projectTask.IsComplete);
                    command.Parameters.AddWithValue("@createDate", projectTask.CreateDate);
                    command.Parameters.AddWithValue("@completeDate", projectTask.CompletedDate);
                    command.Parameters.AddWithValue("@id", projectTask.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
            return projectTask;
        }
    }
}
