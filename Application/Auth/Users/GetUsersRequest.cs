namespace Application.Auth.Users
{
    public class GetUsersRequest : IRequest<IEnumerable<UserDetailsDto>>
    {
    }

    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, IEnumerable<UserDetailsDto>>
    {
        private readonly IUserService _userService;

        public GetUsersRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDetailsDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync();
            return users;
        }
    }
}
