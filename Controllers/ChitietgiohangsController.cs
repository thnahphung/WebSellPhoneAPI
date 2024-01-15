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
    public class ChitietgiohangsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public ChitietgiohangsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Chitietgiohangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chitietgiohang>>> GetChitietgiohangs()
        {
          if (_context.Chitietgiohangs == null)
          {
              return NotFound();
          }
            return await _context.Chitietgiohangs.ToListAsync();
        }

        // GET: api/Chitietgiohangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chitietgiohang>> GetChitietgiohang(int id)
        {
          if (_context.Chitietgiohangs == null)
          {
              return NotFound();
          }
            var chitietgiohang = await _context.Chitietgiohangs.FindAsync(id);

            if (chitietgiohang == null)
            {
                return NotFound();
            }

            return chitietgiohang;
        }

        // GET: api/Chitietgiohangs/Nguoidung/1
        [HttpGet("Nguoidung/{userId}")]
        public async Task<ActionResult<List<Chitietgiohang>>> GetChitietgiohangByUserId(int userId)
        {
            if (_context.Chitietgiohangs == null)
            {
                return NotFound();
            }
            var chitietgiohangs = await _context.Chitietgiohangs.Where(c => c.IdNd == userId && c.Trangthai != 0).ToListAsync();
            
            if (chitietgiohangs == null || !chitietgiohangs.Any())
            {
                return NotFound();
            }
            foreach(Chitietgiohang c in chitietgiohangs)
            {
                c.IdSpNavigation = await _context.Sanphams.FindAsync(c.IdSp);
            }

            return chitietgiohangs;
        }
        // PUT: api/Chitietgiohangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChitietgiohang(int id, Chitietgiohang chitietgiohang)
        {
            if (id != chitietgiohang.Id)
            {
                return BadRequest();
            }

            _context.Entry(chitietgiohang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChitietgiohangExists(id))
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

        // POST: api/Chitietgiohangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chitietgiohang>> PostChitietgiohang(Chitietgiohang chitietgiohang)
        {
          if (_context.Chitietgiohangs == null)
          {
              return Problem("Entity set 'SellPhoneContext.Chitietgiohangs'  is null.");
          }
          
            _context.Chitietgiohangs.Add(chitietgiohang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChitietgiohangExists(chitietgiohang.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChitietgiohang", new { id = chitietgiohang.Id }, chitietgiohang);
        }

        // DELETE: api/Chitietgiohangs/5
        [HttpDelete("{idND}")]
        public async Task<IActionResult> DeleteChitietgiohang(int idND)
        {
            if (_context.Chitietgiohangs == null)
            {
                return NotFound();
            }
            var chitietgiohangs = await _context.Chitietgiohangs
           .Where(c => c.IdNd == idND && c.Trangthai != 0) 
           .ToListAsync();
            if (chitietgiohangs == null || !chitietgiohangs.Any())
            {
                return NotFound();
            }

            foreach (Chitietgiohang c in chitietgiohangs)
            {

            c.Trangthai = 0;
            }
            await _context.SaveChangesAsync();
  

            return NoContent();
        }

        private bool ChitietgiohangExists(int id)
        {
            return (_context.Chitietgiohangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
