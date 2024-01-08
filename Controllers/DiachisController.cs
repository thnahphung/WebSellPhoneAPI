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
    public class DiachisController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public DiachisController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Diachis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diachi>>> GetDiachis()
        {
          if (_context.Diachis == null)
          {
              return NotFound();
          }
            return await _context.Diachis.ToListAsync();
        }

        // GET: api/Diachis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diachi>> GetDiachi(int id)
        {
          if (_context.Diachis == null)
          {
              return NotFound();
          }
            var diachi = await _context.Diachis.FindAsync(id);

            if (diachi == null)
            {
                return NotFound();
            }

            return diachi;
        }
        // GET: api
        [HttpGet("Nguoidung/{id}")]
        public async Task<ActionResult<List<Diachi>>> GetDiachiByUserId(int id)
        {
            if (_context.Diachis == null)
            {
                return NotFound();
            }
            var diachis = await _context.Diachis
            .Where(d => d.IdNd == id) // Giả sử có một trường NguoidungId trong bảng Diachis để liên kết với người dùng
            .ToListAsync();


            return diachis;
        }

        // PUT: api/Diachis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiachi(int id, Diachi diachi)
        {
            if (id != diachi.Id)
            {
                return BadRequest();
            }

            _context.Entry(diachi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiachiExists(id))
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

        // POST: api/Diachis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diachi>> PostDiachi(Diachi diachi)
        {
          if (_context.Diachis == null)
          {
              return Problem("Entity set 'SellPhoneContext.Diachis'  is null.");
          }
            _context.Diachis.Add(diachi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiachi", new { id = diachi.Id }, diachi);
        }

        // DELETE: api/Diachis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiachi(int id)
        {
            if (_context.Diachis == null)
            {
                return NotFound();
            }
            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi == null)
            {
                return NotFound();
            }

            _context.Diachis.Remove(diachi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiachiExists(int id)
        {
            return (_context.Diachis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
