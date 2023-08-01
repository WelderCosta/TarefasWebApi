using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Service.Interfaces;

namespace TaskManager.Service.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository taskRepository;

        public ProjectTaskService(IProjectTaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await taskRepository.GetAllTasksAsync();
        }

        public async Task<ProjectTask> GetTaskAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID da tarefa deve ser maior que zero.");

            ProjectTask task = await taskRepository.GetTaskAsync(id);

            return task ?? throw new Exception($"Nenhuma tarefa encontrada com o ID: {id}");
        }

        public async Task<ProjectTask> AddTaskAsync(ProjectTask projectTask)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(projectTask.Title))
                throw new ArgumentException("O título da tarefa não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(projectTask.Description))
                throw new ArgumentException("A descrição da tarefa não pode estar vazia.");

            return await taskRepository.AddTaskAsync(projectTask);
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask projectTask)
        {
            if (projectTask.Id <= 0)
                throw new ArgumentException("O ID da tarefa deve ser maior que zero.");

            // Validação básica
            if (string.IsNullOrWhiteSpace(projectTask.Title))
                throw new ArgumentException("O título da tarefa não pode estar vazio.");

            if (string.IsNullOrWhiteSpace(projectTask.Description))
                throw new ArgumentException("A descrição da tarefa não pode estar vazia.");

            ProjectTask existingTask = await taskRepository.GetTaskAsync(projectTask.Id);

            if (existingTask == null)
                throw new Exception($"Nenhuma tarefa encontrada com o ID: {projectTask.Id}");

            return await taskRepository.UpdateTaskAsync(projectTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID da tarefa deve ser maior que zero.");

            _ = await taskRepository.GetTaskAsync(id) ?? throw new ArgumentException($"Nenhuma tarefa encontrada com o ID: {id}");

            await taskRepository.DeleteTaskAsync(id);
        }
    }
}
