using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Models;

namespace WebSellPhoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChitietdonhangsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public ChitietdonhangsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Chitietdonhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chitietdonhang>>> GetChitietdonhangs()
        {
          if (_context.Chitietdonhangs == null)
          {
              return NotFound();
          }
            return await _context.Chitietdonhangs.ToListAsync();
        }

        // GET: api/Chitietdonhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chitietdonhang>> GetChitietdonhang(int id)
        {
          if (_context.Chitietdonhangs == null)
          {
              return NotFound();
          }
            var chitietdonhang = await _context.Chitietdonhangs.FindAsync(id);

            if (chitietdonhang == null)
            {
                return NotFound();
            }

            return chitietdonhang;
        }

        // PUT: api/Chitietdonhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChitietdonhang(int id, Chitietdonhang chitietdonhang)
        {
            if (id != chitietdonhang.Id)
            {
                return BadRequest();
            }

            _context.Entry(chitietdonhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChitietdonhangExists(id))
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

        // POST: api/Chitietdonhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chitietdonhang>> PostChitietdonhang(Chitietdonhang chitietdonhang)
        {
          if (_context.Chitietdonhangs == null)
          {
              return Problem("Entity set 'SellPhoneContext.Chitietdonhangs'  is null.");
          }
            _context.Chitietdonhangs.Add(chitietdonhang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChitietdonhangExists(chitietdonhang.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChitietdonhang", new { id = chitietdonhang.Id }, chitietdonhang);
        }

        // DELETE: api/Chitietdonhangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChitietdonhang(int id)
        {
            if (_context.Chitietdonhangs == null)
            {
                return NotFound();
            }
            var chitietdonhang = await _context.Chitietdonhangs.FindAsync(id);
            if (chitietdonhang == null)
            {
                return NotFound();
            }

            _context.Chitietdonhangs.Remove(chitietdonhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChitietdonhangExists(int id)
        {
            return (_context.Chitietdonhangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
