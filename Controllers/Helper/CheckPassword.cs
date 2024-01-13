using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSellPhoneAPI.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebSellPhoneAPI.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPassword : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetCheckPassword([FromQuery] string pass = null,
                                                      [FromQuery] string passhashed = null)
        {
            if (pass == null || passhashed == null || !BCryptNet.Verify(pass, passhashed))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }

}
