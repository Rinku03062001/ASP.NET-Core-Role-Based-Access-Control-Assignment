using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
    }
}
