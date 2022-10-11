using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iapex.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iapex.Models;
using Microsoft.AspNetCore.Authorization;

namespace iapex.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private DataContext _context = null;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var Usuarios = await _context.User.Where(x => x.Correo != "iapex@gmail.com").ToListAsync();
            return Usuarios;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            var userId = await _context.User.FindAsync(id);
            if (userId == null)
            {
                return NotFound();
            }

            return userId;
        }


        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUsers(User user)
        {
            user.Rol = "Personal";

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }


        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsers(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            Hospital hospital = await _context.Hospital.Where(x => x.Id_user == id).FirstOrDefaultAsync();
            Institution institution = await _context.Institution.Where(x => x.Id_user == id).FirstOrDefaultAsync();
            var user = await _context.User.FindAsync(id);
            if (user == null || hospital != null || institution != null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // extras

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }


    }
}