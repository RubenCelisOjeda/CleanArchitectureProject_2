using Application.Common.Exceptions;

namespace Application.Auth.Users
{
    public class GetUserByIdRequest : IRequest<UserDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, UserDetailsDto>
    {
        private readonly IUserService _userService;

        public GetUserByIdRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDetailsDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
    }
}
