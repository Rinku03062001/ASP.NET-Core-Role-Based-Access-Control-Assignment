using ASP.NET_Core_Role_Based_Access_Control_Assignment.Data;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet("projects/{projectId}/tasks")]
        public IActionResult GetTasks(Guid projectId)
        {
            var tasks = _db.Tasks.Where(t => t.ProjectId == projectId);
            return Ok(tasks);
        }



        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        [HttpPost("projects/{projectId}/tasks")]
        public IActionResult CreateTask(Guid projectId, ProjectTask task)
        {
            task.ProjectId = projectId;
            _db.Tasks.Add(task);
            _db.SaveChanges();
            return Ok(task);
        }



        [Authorize(Roles = "Admin,ProjectManager,Developer")]
        [HttpPut("tasks/{id}")]
        public IActionResult UpdateTask(Guid id, ProjectTask updated)
        {
            var task = _db.Tasks.Find(id);
            if(task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            task.Title = updated.Title;
            task.Description = updated.Description;
            task.Status = updated.Status;
            _db.SaveChanges();
            return Ok(task);
        }



        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpDelete("tasks/{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var task = _db.Tasks.Find(id);
            if(task == null)
            {
                return NotFound(new { message = "Task not found" });
            }   

            _db.Tasks.Remove(task);
            _db.SaveChanges();

            return Ok(new { message = "Task deleted successfully" });
        }
    }
}
