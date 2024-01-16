using Application.Auth.Tokens;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            return await Mediator.Send(new GetTokenRequest { TokenRequest = request });
        }
    }
}
