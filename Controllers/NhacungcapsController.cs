using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Controllers.Helper;
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
            return await _context.Nhacungcaps.Where(ncc => ncc.Trangthai == 1).ToListAsync();
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

            if (string.IsNullOrEmpty(nhacungcap.TepHinhAnh))
            {
                if (!string.IsNullOrEmpty(nhacungcap.Diachi))
                {
                    string newFileName = nhacungcap.TenNcc.ToLower() + "-" + "ico" + Path.GetExtension(Global.GetFileNameFromUrl(nhacungcap.Diachi)).ToLower();
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Global.GetFileNameFromUrl(nhacungcap.Diachi));
                    Global.changeFileName(oldPath, newFileName);
                    nhacungcap.Diachi = "http://103.77.214.148/images/" + newFileName;
                }

                _context.Nhacungcaps.Update(nhacungcap);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            string fileName = nhacungcap.TenNcc.ToLower() + "-" + "ico" + Path.GetExtension(nhacungcap.TenTepHinhAnh).ToLower();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            Global.SaveImg(nhacungcap.TepHinhAnh, path);

            nhacungcap.TepHinhAnh = null;
            nhacungcap.Diachi = "http://103.77.214.148/images/" + fileName;
            _context.Nhacungcaps.Update(nhacungcap);
            await _context.SaveChangesAsync();
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
            if (string.IsNullOrEmpty(nhacungcap.TepHinhAnh))
            {
                await _context.Nhacungcaps.AddAsync(nhacungcap);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetNhacungcap", new { id = nhacungcap.Id }, nhacungcap);
            }

            string fileName = nhacungcap.TenNcc.ToLower() + "-" + "ico" + Path.GetExtension(nhacungcap.TenTepHinhAnh).ToLower();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            Global.SaveImg(nhacungcap.TepHinhAnh, path);

            nhacungcap.TepHinhAnh = null;

            nhacungcap.Diachi = "http://103.77.214.148/images/" + fileName;

            await _context.Nhacungcaps.AddAsync(nhacungcap);
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
            nhacungcap.Trangthai = 0;
            if (nhacungcap == null)
            {
                return NotFound();
            }

            _context.Nhacungcaps.Update(nhacungcap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhacungcapExists(int id)
        {
            return (_context.Nhacungcaps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
