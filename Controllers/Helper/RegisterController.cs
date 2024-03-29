﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebSellPhoneAPI.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public RegisterController(SellPhoneContext context)
        {
            _context = context;
        }

        // GET: api/Register/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nguoidung>> GetNguoidung(int id)
        {
            if (_context.Nguoidungs == null)
            {
                return NotFound();
            }
            var nguoidung = await _context.Nguoidungs.FindAsync(id);

            if (nguoidung == null)
            {
                return NotFound();
            }

            return nguoidung;
        }

        // POST: api/Register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nguoidung>> PostNguoidung(Nguoidung nguoidung)
        {
            if (_context.Nguoidungs == null)
            {
                return Problem("Entity set 'SellPhoneContext.Nguoidungs'  is null.");
            }

            var existingUser = _context.Nguoidungs.FirstOrDefault(u => u.Sdt == nguoidung.Sdt);
            if (existingUser != null)
            {
                return BadRequest("Số điện thoại đã được đăng kí.");
            }

            nguoidung.Matkhau = BCryptNet.HashPassword(nguoidung.Matkhau);
            nguoidung.Quyen = 0;
            nguoidung.Ngaytao = DateTime.Now;
            nguoidung.Ngaycapnhat = DateTime.Now;
            nguoidung.Trangthai = 1;
            _context.Nguoidungs.Add(nguoidung);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNguoidung", new { id = nguoidung.Id }, nguoidung);
        }


        private bool NguoidungExists(int id)
        {
            return (_context.Nguoidungs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
