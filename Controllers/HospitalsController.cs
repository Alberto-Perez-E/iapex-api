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
    public class HospitalsController : ControllerBase
    {
        private DataContext _context = null;
        public HospitalsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Hospital
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            return await _context.Hospital.ToListAsync();
        }

        // GET: api/Hospital/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospitals(int id)
        {
            var hospitalId = await _context.Hospital.FindAsync(id);
            if (hospitalId == null)
            {
                return NotFound();
            }

            return hospitalId;
        }


        // POST: api/Hospital
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospitals(Hospital hospital)
        {
            _context.Hospital.Add(hospital);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetHospitals), new { id = hospital.Id }, hospital);
        }


        // PUT: api/Hospital/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutHospitals(int id, Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return BadRequest();
            }
            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (HospitalExists(id))
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


        // DELETE: api/Hospital/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitals(int id)
        {
            var hospital = await _context.Hospital.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospital.Remove(hospital);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // extras

        private bool HospitalExists(int id)
        {
            return _context.Hospital.Any(e => e.Id == id);
        }


    }
}