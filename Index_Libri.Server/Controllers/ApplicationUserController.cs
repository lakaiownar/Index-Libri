using Index_Libri.Server.BLL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Index_Libri.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to register a new user
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] ApplicationUser user)
        {
            // Check if the user already exists
            var existingUser = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            if (existingUser != null)
            {
                // User exists, update the token
                existingUser.Token = user.Token;
            }
            else
            {
                // User does not exist, add the new user to the database
                _context.ApplicationUser.Add(user);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { registered = true });
        }

    }
}
