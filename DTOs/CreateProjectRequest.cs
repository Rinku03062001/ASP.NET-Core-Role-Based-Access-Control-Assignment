using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.DTOs
{
    public class CreateProjectRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid ProjectManagerId { get; set; }
        public List<Guid> DeveloperIds { get; set; } = new();
        public Enums.ProjectStatus Status { get; set; }
    }
}
