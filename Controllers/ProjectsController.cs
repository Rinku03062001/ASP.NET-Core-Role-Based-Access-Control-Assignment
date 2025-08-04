using ASP.NET_Core_Role_Based_Access_Control_Assignment.Data;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.DTOs;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProjectsController(ApplicationDbContext db)
        {
            _db = db;
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Projects.ToList());   
        }



        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_db.Projects.Find(id));
        }


        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPost]
        public IActionResult Create(CreateProjectRequest project)
        {
            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = project.Name,
                Description = project.Description,
                ProjectManagerId = project.ProjectManagerId,
                DeveloperIds = project.DeveloperIds,
                Status = project.Status,
                CreatedAt = DateTime.UtcNow
            };

            _db.Projects.Add(newProject);
            _db.SaveChanges();
            return Ok(newProject);
        }


        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPut("{id}")]
        public IActionResult update(Guid id, Project updated)
        {
            var project = _db.Projects.Find(id);
            if(project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            project.Name = updated.Name;
            project.Description = updated.Description;
            project.Status = updated.Status;
            _db.SaveChanges();
            return Ok(project);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var project = _db.Projects.Find(id);
            if(project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            _db.Projects.Remove(project);
            _db.SaveChanges();
            return Ok(new { message = "Project Deleted Successfully" });
        }
    }
}
