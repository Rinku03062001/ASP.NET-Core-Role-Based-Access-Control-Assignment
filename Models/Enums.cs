namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Models
{
    public class Enums
    {
        public enum  UserRole
        {
            Admin,
            ProjectManager,
            Developer,
            Viewer
        }

        public enum ProjectStatus 
        {
            NotStarted,
            InProgress,
            Completed
        }

        public enum TaskStatus
        {
            ToDo,
            InProgress,
            Done
        }
    }
}
