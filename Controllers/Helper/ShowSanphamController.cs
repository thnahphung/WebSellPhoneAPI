using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Entities;

namespace WebSellPhoneAPI.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowSanphamController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public ShowSanphamController(SellPhoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetSanphams()
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }

            var groupedProducts = _context.Sanphams.Include(sp => sp.Hinhanhs)
                                                    .GroupBy(p => p.Tenviettat)
                                                    .Select(group => new
                                                    {
                                                        Tenviettat = group.Key,
                                                        Thongtin = group.First(),
                                                        PhanLoai = group.ToArray()
                                                    });

            return await Task.FromResult(groupedProducts.ToList());
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<object>>> GetSanphams([FromQuery] string tenviettat = null,
                                                                         [FromQuery] string order = null,
                                                                         [FromQuery] int page = 1,
                                                                         [FromQuery] int range = 10)
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }

            var groupedProducts = _context.Sanphams.GroupBy(p => p.Tenviettat)
                .Select(group => new
                {
                    Tenviettat = group.Key,
                    Thongtin = group.First(),
                    Gia = group.First().Giadagiam,
                    PhanLoai = group.ToArray()
                });

            var filteredProducts = groupedProducts.Where(group => string.IsNullOrEmpty(tenviettat) || group.Tenviettat.Contains(tenviettat));

            if (!string.IsNullOrEmpty(order))
            {
                bool isDescending = order.ToLower() == "desc";
                filteredProducts = isDescending
                    ? filteredProducts.OrderByDescending(group => group.Gia)
                    : filteredProducts.OrderBy(p => p.Gia);
            }

            var pagedProducts = filteredProducts.Skip((page - 1) * range)
                                                .Take(range);

            return await Task.FromResult(pagedProducts.ToList());
        }

        // GET: api/ShowSanpham/iphone-11
        [HttpGet("{tenviettat}")]
        public async Task<ActionResult<object>> GetSanpham(string tenviettat)
        {
            if (_context.Sanphams == null)
            {
                return NotFound();
            }
            var sanphams = await _context.Sanphams.Include(sp => sp.Hinhanhs)
                                                    .Include(s => s.Binhluans).ThenInclude(b => b.IdNdNavigation)
                                                    .Where(sp => sp.Tenviettat == tenviettat)
                                                    .ToListAsync();

            if (sanphams.Count == 0)
            {
                return NotFound();
            }

            var sanpham = new
            {
                Tenviettat = sanphams.First().Tenviettat,
                Thongtin = sanphams.First(),
                PhanLoai = sanphams.ToArray()
            };

            return sanpham;
        }

        private bool SanphamExists(int id)
        {
            return (_context.Sanphams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
