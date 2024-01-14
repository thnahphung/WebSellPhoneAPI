using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Models;

namespace WebSellPhoneAPI.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortSanphamsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public SortSanphamsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/SortSanphamsByPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanpham>>> GetSanphamsSortedByPrice()
        {
          if (_context.Sanphams == null)
          {
              return NotFound();
          }
            return await _context.Sanphams.OrderBy(s => -s.Giagoc).ToListAsync();
        }

        // GET: api/GetSanphamsByThuongHieu
        [HttpGet("ThuongHieu/{idNCC}")]
        public async Task<ActionResult<List<Sanpham>>> GetSanphamsByBrand(int idNCC)
        {
          if (_context.Sanphams == null)
          {
              return NotFound();
          }
            var sanphams = await _context.Sanphams.Where(s => s.IdNcc == idNCC && s.Trangthai != 0).ToListAsync();

            if (sanphams == null)
            {
                return NotFound();
            }

            return sanphams;
        }

        // PUT: api/SortSanphams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanpham(int id, Sanpham sanpham)
        {
            if (id != sanpham.Id)
            {
                return BadRequest();
            }

            _context.Entry(sanpham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanphamExists(id))
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

        // POST: api/SortSanphams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sanpham>> PostSanpham(Sanpham sanpham)
        {
          if (_context.Sanphams == null)
          {
              return Problem("Entity set 'SellPhoneContext.Sanphams'  is null.");
          }
            _context.Sanphams.Add(sanpham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanpham", new { id = sanpham.Id }, sanpham);
        }

        // DELETE: api/SortSanphams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanpham(int id)
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanphamExists(int id)
        {
            return (_context.Sanphams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
