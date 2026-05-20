using LearnForge.Server.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LearnForge.Server.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [HttpGet("Encode")]
        public async Task<string> Encode(string input)
        {
            return await Security.CustomBase64Encode(input);
        }

        [HttpGet("Decode")]
        public async Task<string> Decode(string input)
        {
            return await Security.CustomBase64Decode(input);
        }
    }
}
