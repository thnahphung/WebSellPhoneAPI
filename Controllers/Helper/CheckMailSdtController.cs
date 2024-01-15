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
    public class CheckMailSdtController : ControllerBase
    {
        private readonly SellPhoneContext _context;

        public CheckMailSdtController(SellPhoneContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetCheckMailSdt([FromQuery] string email = null,
                                                      [FromQuery] string sdt = null)
        {
            var nguoidung = _context.Nguoidungs.Where(nd => nd.Email == email && nd.Sdt == sdt).FirstOrDefault();
            if (nguoidung == null)
            {
                return BadRequest();
            }
            return Ok(nguoidung);
        }

    }
}
