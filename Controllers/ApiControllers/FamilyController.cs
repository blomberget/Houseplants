using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Houseplants.Models;

namespace Houseplants.Controllers.ApiControllers
{
    [Route("api/family")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly HouseplantsContext _context;

        public FamilyController(HouseplantsContext context)
        {
            _context = context;
        }

        // GET: api/Family
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamily()
        {
            return await _context.Family.ToListAsync();
        }

        // GET: api/Family/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await _context.Family.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return family;
        }

        // PUT: api/Family/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamily(int id, Family family)
        {
            if (id != family.FamilyId)
            {
                return BadRequest();
            }

            _context.Entry(family).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/Family
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Family>> PostFamily(Family family)
        {
            _context.Family.Add(family);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamily", new { id = family.FamilyId }, family);
        }

        // DELETE: api/Family/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(int id)
        {
            var family = await _context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            _context.Family.Remove(family);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.FamilyId == id);
        }
    }
}
