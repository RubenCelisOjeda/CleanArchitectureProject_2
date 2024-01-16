using Application.Common.Interfaces;

namespace Application.Auth.Tokens
{
    public interface ITokenService : ITransientService
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest request);
    }
}
