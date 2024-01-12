using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebSellPhoneAPI.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public LoginController(SellPhoneContext context)
        {
            _context = context;
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nguoidung>> PostNguoidung(Nguoidung nguoidung)
        {
            if (_context.Nguoidungs == null)
            {
                return Problem("Entity set 'SellPhoneContext.Nguoidungs'  is null.");
            }

            Nguoidung existingUser = null;

            if (nguoidung.GoogleId != null)
            {
                existingUser = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.GoogleId == nguoidung.GoogleId);
                if (existingUser == null)
                {
                    nguoidung.Quyen = 0;
                    nguoidung.Ngaytao = DateTime.Now;
                    nguoidung.Ngaycapnhat = DateTime.Now;
                    nguoidung.Trangthai = 1;
                    _context.Nguoidungs.Add(nguoidung);
                    await _context.SaveChangesAsync();
                    existingUser = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.GoogleId == nguoidung.GoogleId);
                }
            }

            if (nguoidung.FacebookId != null)
            {
                existingUser = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.FacebookId == nguoidung.FacebookId);
                if (existingUser == null)
                {
                    nguoidung.Quyen = 0;
                    nguoidung.Ngaytao = DateTime.Now;
                    nguoidung.Ngaycapnhat = DateTime.Now;
                    nguoidung.Trangthai = 1;
                    _context.Nguoidungs.Add(nguoidung);
                    await _context.SaveChangesAsync();
                    existingUser = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.FacebookId == nguoidung.FacebookId);
                }
            }

            if (nguoidung.Matkhau != null && nguoidung.Sdt != null)
            {
                existingUser = await _context.Nguoidungs.FirstOrDefaultAsync(u => u.Sdt == nguoidung.Sdt);
                if (existingUser == null || !BCryptNet.Verify(nguoidung.Matkhau, existingUser.Matkhau))
                {
                    existingUser = null;
                }
            }

            if (existingUser == null)
            {
                return NotFound();
            }

            return existingUser;
        }

        private bool NguoidungExists(int id)
        {
            return (_context.Nguoidungs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
