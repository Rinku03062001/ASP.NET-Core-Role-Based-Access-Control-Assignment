using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.Data;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.Services;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.Models;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.DTOs;
using Microsoft.AspNetCore.Authorization;
using Serilog;


namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthController(ApplicationDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }


        // Code for Login endpoint
        [HttpPost("login")] 
        public IActionResult Login(LoginDto dto)
        {
            Log.Information("Login attempt for {Email}", dto.Email);

            if (dto.Email == null || dto.Password == null)
            {
                return BadRequest(new { message = "Email and password are required" });
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || user.PasswordHash == null)
            {
                Log.Warning("Login failed for {Email}: User not found or password hash is null", dto.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                Log.Warning("Login failed for {Email}: Invalid password", dto.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _tokenService.GenerateToken(user);
            Log.Information("Login successful for {Email}", dto.Email); 
            return Ok(new { token });
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (dto.Password == null)
            {
                Log.Warning("Registration failed: Password is required");
                return BadRequest(new { message = "Password is required" });
            }

            if (_db.Users.Any(u => u.Email == dto.Email))
            {
                Log.Warning("Registration failed: Email {Email} already exists", dto.Email);
                return BadRequest(new { message = "Email already exists" });
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Role = dto.Role,
                PasswordHash = _hasher.HashPassword(new User(), dto.Password) // Provide a non-null User instance
            };

            _db.Users.Add(user);
            _db.SaveChanges();
            Log.Information("New user registered: {Email}", dto.Email);
            return Ok(new { message = "User registered successfully" });
        }
    }
}
