namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public Enums.UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
