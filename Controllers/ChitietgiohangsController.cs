﻿using System;
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

        // PUT: api/Chitietgiohangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChitietgiohang(int id, Chitietgiohang chitietgiohang)
        {
            if (id != chitietgiohang.IdNd)
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
                if (ChitietgiohangExists(chitietgiohang.IdNd))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChitietgiohang", new { id = chitietgiohang.IdNd }, chitietgiohang);
        }

        // DELETE: api/Chitietgiohangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChitietgiohang(int id)
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

            _context.Chitietgiohangs.Remove(chitietgiohang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChitietgiohangExists(int id)
        {
            return (_context.Chitietgiohangs?.Any(e => e.IdNd == id)).GetValueOrDefault();
        }
    }
}