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
    public class BinhluansController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public BinhluansController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Binhluans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Binhluan>>> GetBinhluans()
        {
          if (_context.Binhluans == null)
          {
              return NotFound();
          }
            return await _context.Binhluans.ToListAsync();
        }

        // GET: api/Binhluans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Binhluan>> GetBinhluan(int id)
        {
          if (_context.Binhluans == null)
          {
              return NotFound();
          }
            var binhluan = await _context.Binhluans.FindAsync(id);

            if (binhluan == null)
            {
                return NotFound();
            }

            return binhluan;
        }

        // PUT: api/Binhluans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBinhluan(int id, Binhluan binhluan)
        {
            if (id != binhluan.Id)
            {
                return BadRequest();
            }

            _context.Entry(binhluan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinhluanExists(id))
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

        // POST: api/Binhluans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Binhluan>> PostBinhluan(Binhluan binhluan)
        {
          if (_context.Binhluans == null)
          {
              return Problem("Entity set 'SellPhoneContext.Binhluans'  is null.");
          }
            _context.Binhluans.Add(binhluan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBinhluan", new { id = binhluan.Id }, binhluan);
        }

        // DELETE: api/Binhluans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBinhluan(int id)
        {
            if (_context.Binhluans == null)
            {
                return NotFound();
            }
            var binhluan = await _context.Binhluans.FindAsync(id);
            if (binhluan == null)
            {
                return NotFound();
            }

            _context.Binhluans.Remove(binhluan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BinhluanExists(int id)
        {
            return (_context.Binhluans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
