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
    public class DatHangController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public DatHangController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Donhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donhang>>> GetDonhangs()
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
            return await _context.Donhangs.ToListAsync();
        }

        // GET: api/Donhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donhang>> GetDonhang(int id)
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
            var donhang = await _context.Donhangs.FindAsync(id);

            if (donhang == null)
            {
                return NotFound();
            }

            return donhang;
        }

        // PUT: api/Donhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonhang(int id, Donhang donhang)
        {
            if (id != donhang.Id)
            {
                return BadRequest();
            }

            _context.Entry(donhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonhangExists(id))
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

        // POST: api/Donhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donhang>> PostDonhang(Donhang donhang)
        {
          if (_context.Donhangs == null)
          {
              return Problem("Entity set 'SellPhoneContext.Donhangs'  is null.");
          }

            _context.Donhangs.Add(donhang);
            await _context.SaveChangesAsync();

            var chitietgiohangs = await _context.Chitietgiohangs.Include(s => s.IdSpNavigation).Where(c => c.IdNd == donhang.IdNd && c.Trangthai != 0).ToListAsync();
           // Get product in the cart TO GET PRICE
           
            foreach(Chitietgiohang c in chitietgiohangs)
            {
            Chitietdonhang chitietdonhang = new Chitietdonhang();
                chitietdonhang.IdDh = donhang.Id;
            chitietdonhang.IdSp = c.IdSp;
            chitietdonhang.Soluong = c.Soluong;
            chitietdonhang.Dongia = c.IdSpNavigation.Giadagiam * c.Soluong;
            _context.Chitietdonhangs.Add(chitietdonhang);
            }
            foreach(Chitietgiohang c in chitietgiohangs)
            {
                 c.Trangthai = 0;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonhang", new { id = donhang.Id }, donhang);
        }

        // DELETE: api/Donhangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonhang(int id)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var donhang = await _context.Donhangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }

            _context.Donhangs.Remove(donhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonhangExists(int id)
        {
            return (_context.Donhangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
