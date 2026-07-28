using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResellerApp.Api.Interfaces;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EbayController : ControllerBase
    {
        private readonly IEbayService _ebayService;

        public EbayController(IEbayService ebayService)
        {
            _ebayService = ebayService;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var token = await _ebayService.GetApplicationTokenAsync();

            return Ok(new
            {
                token.TokenType,
                token.ExpiresIn,
                AccessTokenPreview = token.AccessToken.Substring(0, 25) + "..."
            });
        }
    }
}