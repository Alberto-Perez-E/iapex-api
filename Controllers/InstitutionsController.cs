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
    public class InstitutionsController : ControllerBase
    {
        private DataContext _context = null;
        public InstitutionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Institution
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetInstitutions()
        {
            return await _context.Institution.ToListAsync();
        }

        // GET: api/Institution/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> GetInstitutions(int id)
        {
            var institutionId = await _context.Institution.FindAsync(id);
            if (institutionId == null)
            {
                return NotFound();
            }

            return institutionId;
        }


        // POST: api/Institution
        [HttpPost]
        public async Task<ActionResult<Institution>> PostInstitutions(Institution institution)
        {
            _context.Institution.Add(institution);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetInstitutions), new { id = institution.Id }, institution);
        }


        // PUT: api/Institution/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutInstitutions(int id, Institution institution)
        {
            if (id != institution.Id)
            {
                return BadRequest();
            }
            _context.Entry(institution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (InstitutionExists(id))
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


        // DELETE: api/Institution/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitutions(int id)
        {
            var institution = await _context.Institution.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }

            _context.Institution.Remove(institution);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // extras

        private bool InstitutionExists(int id)
        {
            return _context.Institution.Any(e => e.Id == id);
        }


    }
}