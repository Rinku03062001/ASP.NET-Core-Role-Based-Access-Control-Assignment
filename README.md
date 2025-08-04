# ASP.NET Core Role-Based Access Control Assignment

This is a RESTful API built with **ASP.NET Core**, demonstrating:
- JWT-based authentication
- Role-Based Access Control (RBAC)
- Entity Framework Core for data access
- Clean architecture with DTOs & models
- API documentation via Swagger

---


**Technologies Used**
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (OpenAPI)

---

**Project Structure**
/Controllers # API endpoints
/Models # Domain models (e.g., User, Project, ProjectTask)
/Models/DTOs # Data Transfer Objects
/Migrations # EF Core migrations
/Enums # Enum definitions (e.g., roles, task status)
/Database # Optional: DB scripts or backups
Program.cs, Startup.cs # App config & DI setup
