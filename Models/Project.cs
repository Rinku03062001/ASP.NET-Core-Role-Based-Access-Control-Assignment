namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid ProjectManagerId { get; set; } 

        public List<Guid> DeveloperIds { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  
        public Enums.ProjectStatus Status { get; set; }
    }
}
