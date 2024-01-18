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
    public class NhacungcapsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public NhacungcapsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Nhacungcaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nhacungcap>>> GetNhacungcaps()
        {
          if (_context.Nhacungcaps == null)
          {
              return NotFound();
          }
            return await _context.Nhacungcaps.ToListAsync();
        }

        // GET: api/Nhacungcaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nhacungcap>> GetNhacungcap(int id)
        {
          if (_context.Nhacungcaps == null)
          {
              return NotFound();
          }
            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);

            if (nhacungcap == null)
            {
                return NotFound();
            }

            return nhacungcap;
        }

        // PUT: api/Nhacungcaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhacungcap(int id, Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.Id)
            {
                return BadRequest();
            }

            _context.Entry(nhacungcap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhacungcapExists(id))
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

        // POST: api/Nhacungcaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nhacungcap>> PostNhacungcap(Nhacungcap nhacungcap)
        {
          if (_context.Nhacungcaps == null)
          {
              return Problem("Entity set 'SellPhoneContext.Nhacungcaps'  is null.");
          }
            _context.Nhacungcaps.Add(nhacungcap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNhacungcap", new { id = nhacungcap.Id }, nhacungcap);
        }

        // DELETE: api/Nhacungcaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhacungcap(int id)
        {
            if (_context.Nhacungcaps == null)
            {
                return NotFound();
            }
            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            nhacungcap.Trangthai = 0;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhacungcapExists(int id)
        {
            return (_context.Nhacungcaps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
