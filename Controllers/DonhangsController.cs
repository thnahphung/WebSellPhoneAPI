﻿using System;
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
    public class DonhangsController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public DonhangsController(SellPhoneContext context)
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
            return await _context.Donhangs.Include(d => d.IdNdNavigation).Include(d => d.IdDcNavigation).Include(dh => dh.Chitietdonhangs).ThenInclude(c => c.IdSpNavigation).ToListAsync();
        }

        // GET: api/Donhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donhang>> GetDonhang(int id)
        {
          if (_context.Donhangs == null)
          {
              return NotFound();
          }
            var donhang = await _context.Donhangs.Include(d => d.IdNdNavigation).Include(d => d.IdDcNavigation).FirstOrDefaultAsync(d => d.Id == id);

            if (donhang == null)
            {
                return NotFound();
            }

            return donhang;
        }
        // GET: api/Donhangs/Nguoidung/5
        [HttpGet("Nguoidung/{userId}")]
        public async Task<ActionResult<List<Donhang>>> GetDonhangByUserId(int userId)
        {
            if (_context.Donhangs == null)
            {
                return NotFound();
            }
            var donhangs = await _context.Donhangs.Where(d => d.IdNd == userId && d.Trangthai != 0).ToListAsync();

            if (donhangs == null)
            {
                return NotFound();
            }
            foreach (Donhang d in donhangs)
            {
                var chitietdonhangs = await _context.Chitietdonhangs.Where(c => c.IdDh == d.Id).ToListAsync();
                foreach (Chitietdonhang c in chitietdonhangs)
                {
                    d.Chitietdonhangs.Add(c);
                }
            }

            return donhangs;
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
            if (donhang.Trangthai == 2)
            {
                donhang.Ngaythanhtoan = DateTime.Now;
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

            foreach (Chitietdonhang c in donhang.Chitietdonhangs)
            {
                c.IdSpNavigation = await _context.Sanphams.FindAsync(c.IdSp);
                c.Dongia = c.IdSpNavigation.Giadagiam * c.Soluong;
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

            donhang.Trangthai = 0;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonhangExists(int id)
        {
            return (_context.Donhangs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
