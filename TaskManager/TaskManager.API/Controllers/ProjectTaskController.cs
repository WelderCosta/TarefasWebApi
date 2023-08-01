using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Service.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService projectTaskService;

        public ProjectTaskController(IProjectTaskService projectTaskService)
        {
            this.projectTaskService = projectTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            IEnumerable<ProjectTask> tasks = await projectTaskService.GetAllTasksAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            ProjectTask task = await projectTaskService.GetTaskAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ProjectTask projectTask)
        {
            if (projectTask == null)
            {
                return BadRequest();
            }

            await projectTaskService.AddTaskAsync(projectTask);
            return CreatedAtAction(nameof(GetTask), new { id = projectTask.Id }, projectTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTask projectTask)
        {
            if (projectTask == null || id !=  projectTask.Id)
            {
                return BadRequest();
            }

            ProjectTask existingTask = await projectTaskService.GetTaskAsync(id);
            
            if (existingTask == null)
            {
                return NotFound();
            }

            await projectTaskService.UpdateTaskAsync(projectTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            ProjectTask existingTask = await projectTaskService.GetTaskAsync(id);
            if(existingTask == null)
            {
                return NotFound();
            }

            await projectTaskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
