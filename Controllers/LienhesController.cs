using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Entities;

namespace WebSellPhoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LienhesController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public LienhesController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Lienhes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lienhe>>> GetLienhes()
        {
          if (_context.Lienhes == null)
          {
              return NotFound();
          }
            return await _context.Lienhes.ToListAsync();
        }

        // GET: api/Lienhes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lienhe>> GetLienhe(int id)
        {
          if (_context.Lienhes == null)
          {
              return NotFound();
          }
            var lienhe = await _context.Lienhes.FindAsync(id);

            if (lienhe == null)
            {
                return NotFound();
            }

            return lienhe;
        }

        // PUT: api/Lienhes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLienhe(int id, Lienhe lienhe)
        {
            if (id != lienhe.Id)
            {
                return BadRequest();
            }

            _context.Entry(lienhe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienheExists(id))
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

        // POST: api/Lienhes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lienhe>> PostLienhe(Lienhe lienhe)
        {
          if (_context.Lienhes == null)
          {
              return Problem("Entity set 'SellPhoneContext.Lienhes'  is null.");
          }
            _context.Lienhes.Add(lienhe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLienhe", new { id = lienhe.Id }, lienhe);
        }

        // DELETE: api/Lienhes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLienhe(int id)
        {
            if (_context.Lienhes == null)
            {
                return NotFound();
            }
            var lienhe = await _context.Lienhes.FindAsync(id);
            if (lienhe == null)
            {
                return NotFound();
            }

            _context.Lienhes.Remove(lienhe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LienheExists(int id)
        {
            return (_context.Lienhes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
