namespace Application.Auth.Tokens
{
    public class GetTokenRequest : IRequest<TokenResponse>
    {
        public TokenRequest TokenRequest { get; set; }
    }

    public class GetTokenRequestHandler : IRequestHandler<GetTokenRequest, TokenResponse>
    {
        private readonly ITokenService _tokenService;

        public GetTokenRequestHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> Handle(GetTokenRequest request, CancellationToken cancellationToken)
        {
            return await _tokenService.GetTokenAsync(request.TokenRequest);
        }
    }
}
