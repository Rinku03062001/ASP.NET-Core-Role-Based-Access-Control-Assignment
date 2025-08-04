namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Models
{
    public class ProjectTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Guid ProjectId { get; set; }
        public Guid? AssignedToId { get; set; }
        public Enums.TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  
        public DateTime? DueDate { get; set; } 
    }
}
