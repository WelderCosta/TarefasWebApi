using TaskManager.Core.Models;

namespace TaskManager.Core.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTask> GetTaskAsync(int id);
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask> AddTaskAsync(ProjectTask projectTask);
        Task<ProjectTask> UpdateTaskAsync(ProjectTask projectTask);
        Task DeleteTaskAsync(int id);
    }
}
