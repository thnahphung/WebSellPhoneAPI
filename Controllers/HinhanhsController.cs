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
    public class HinhanhsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public HinhanhsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Hinhanhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hinhanh>>> GetHinhanhs()
        {
          if (_context.Hinhanhs == null)
          {
              return NotFound();
          }
            return await _context.Hinhanhs.ToListAsync();
        }

        // GET: api/Hinhanhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hinhanh>> GetHinhanh(int id)
        {
          if (_context.Hinhanhs == null)
          {
              return NotFound();
          }
            var hinhanh = await _context.Hinhanhs.FindAsync(id);

            if (hinhanh == null)
            {
                return NotFound();
            }

            return hinhanh;
        }

        // PUT: api/Hinhanhs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHinhanh(int id, Hinhanh hinhanh)
        {
            if (id != hinhanh.Id)
            {
                return BadRequest();
            }

            _context.Entry(hinhanh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HinhanhExists(id))
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

        // POST: api/Hinhanhs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hinhanh>> PostHinhanh(Hinhanh hinhanh)
        {
          if (_context.Hinhanhs == null)
          {
              return Problem("Entity set 'SellPhoneContext.Hinhanhs'  is null.");
          }
            _context.Hinhanhs.Add(hinhanh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHinhanh", new { id = hinhanh.Id }, hinhanh);
        }

        // DELETE: api/Hinhanhs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHinhanh(int id)
        {
            if (_context.Hinhanhs == null)
            {
                return NotFound();
            }
            var hinhanh = await _context.Hinhanhs.FindAsync(id);
            if (hinhanh == null)
            {
                return NotFound();
            }

            _context.Hinhanhs.Remove(hinhanh);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HinhanhExists(int id)
        {
            return (_context.Hinhanhs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
