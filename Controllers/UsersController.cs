using ASP.NET_Core_Role_Based_Access_Control_Assignment.Data;
using ASP.NET_Core_Role_Based_Access_Control_Assignment.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP.NET_Core_Role_Based_Access_Control_Assignment.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller // Renamed class from AuthController to UsersController
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_db.Users.ToList());
        }


        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim is missing.");
            }

            var userId = Guid.Parse(userIdClaim);
            var user = _db.Users.Find(userId);
            return Ok(user);
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public IActionResult UpdateUserRole(Guid id, UpdateRoleDto dto)
        {
            var user = _db.Users.Find(id);
            if(user == null)
            {
                return NotFound(new {message = "The user is not Found"});
            }

            user.Role = dto.Role;
            _db.SaveChanges();
            return Ok(user);
        }
    }
}
