using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.DTOs
{
    public class RegisterDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }  
        public string? Password { get; set; }

        public Enums.UserRole Role { get; set; } 
    }
}
