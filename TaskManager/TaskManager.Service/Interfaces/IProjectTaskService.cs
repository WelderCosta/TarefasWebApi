using TaskManager.Core.Models;

namespace TaskManager.Service.Interfaces
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask> GetTaskAsync(int id);
        Task<ProjectTask> AddTaskAsync(ProjectTask projectTask);
        Task<ProjectTask> UpdateTaskAsync(ProjectTask projectTask);
        Task DeleteTaskAsync(int id);
    }
}
