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
    public class SanphamsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public SanphamsController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Sanphams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanpham>>> GetSanphams()
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }
            var sanphams = await _context.Sanphams.Include(sp => sp.Hinhanhs).ToListAsync();
            return sanphams;
        }

        // GET: api/Sanphams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sanpham>> GetSanpham(int id)
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams.Include(sp => sp.Hinhanhs).FirstOrDefaultAsync(sp => sp.Id == id);

            if (sanpham == null)
            {
                return NotFound();
            }

            return sanpham;
        }

        // PUT: api/Sanphams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanpham(int id, [FromForm] Sanpham sanpham)
        {
            if (id != sanpham.Id)
            {
                return BadRequest();
            }

            if (sanpham.TepHinhAnh.Length > 0)
            {
                string fileName = sanpham.Tenviettat + Path.GetExtension(sanpham.TepHinhAnh.FileName).ToLower();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await sanpham.TepHinhAnh.CopyToAsync(stream);
                }

                sanpham.TepHinhAnh = null;
                _context.Sanphams.Update(sanpham);
                await _context.SaveChangesAsync();

                var hinhanh = _context.Hinhanhs.FirstOrDefault(h => h.IdSp == sanpham.Id);
                if (hinhanh != null)
                {
                    hinhanh.Url = "/images/" + fileName;
                    _context.Hinhanhs.Update(hinhanh);
                }
                else
                {
                    hinhanh = new Hinhanh();
                    hinhanh.IdSp = sanpham.Id;
                    hinhanh.Url = "http://103.77.214.148/images/" + fileName;
                    _context.Hinhanhs.Add(hinhanh);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Sanphams.Update(sanpham);
                await _context.SaveChangesAsync();
            }

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

        // POST: api/Sanphams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sanpham>> PostSanpham([FromForm] Sanpham sanpham)
        {
            if (_context.Sanphams == null)
            {
                return Problem("Entity set 'SellPhoneContext.Sanphams'  is null.");
            }

            if (sanpham.TepHinhAnh.Length > 0)
            {
                string fileName = sanpham.Tenviettat + Path.GetExtension(sanpham.TepHinhAnh.FileName).ToLower();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await sanpham.TepHinhAnh.CopyToAsync(stream);
                }

                sanpham.TepHinhAnh = null;
                await _context.Sanphams.AddAsync(sanpham);
                await _context.SaveChangesAsync();

                Hinhanh hinhanh = new Hinhanh();
                hinhanh.IdSp = sanpham.Id;
                hinhanh.Url = "http://103.77.214.148/images/" + fileName;

                _context.Hinhanhs.Add(hinhanh);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Sanphams.Add(sanpham);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetSanpham", new { id = sanpham.Id }, sanpham);
        }

        // DELETE: api/Sanphams/5
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
            sanpham.Trangthai = 0;

            _context.Sanphams.Update(sanpham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanphamExists(int id)
        {
            return (_context.Sanphams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
