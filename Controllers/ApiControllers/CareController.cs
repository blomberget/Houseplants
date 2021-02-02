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
    [Route("api/[controller]")]
    [ApiController]
    public class CareController : ControllerBase
    {
        private readonly HouseplantsContext _context;

        public CareController(HouseplantsContext context)
        {
            _context = context;
        }

        // GET: api/Care
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Care>>> GetCare()
        {
            return await _context.Care.ToListAsync();
        }

        // GET: api/Care/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Care>> GetCare(int id)
        {
            var care = await _context.Care.FindAsync(id);

            if (care == null)
            {
                return NotFound();
            }

            return care;
        }

        // PUT: api/Care/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCare(int id, Care care)
        {
            if (id != care.CareId)
            {
                return BadRequest();
            }

            _context.Entry(care).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareExists(id))
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

        // POST: api/Care
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Care>> PostCare(Care care)
        {
            _context.Care.Add(care);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCare", new { id = care.CareId }, care);
        }

        // DELETE: api/Care/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCare(int id)
        {
            var care = await _context.Care.FindAsync(id);
            if (care == null)
            {
                return NotFound();
            }

            _context.Care.Remove(care);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CareExists(int id)
        {
            return _context.Care.Any(e => e.CareId == id);
        }
    }
}
