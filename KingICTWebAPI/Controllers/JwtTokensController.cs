using KingICTWebAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KingICTWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JwtTokensController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetToken")]
        public ActionResult GetToken()
        {
            try
            {
                // The same secure key must be used here to create JWT,
                // as the one that is used by middleware to verify JWT
                var secureKey = _configuration["JWT:SecureKey"];
                var serializedToken = JwtTokenProvider.CreateToken(secureKey, 10);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
