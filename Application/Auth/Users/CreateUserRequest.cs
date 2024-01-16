using Domain;

namespace Application.Auth.Users
{
    public class CreateUserRequest : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OrganizationId { get; set; }
    }

    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, int>
    {
        private readonly IUserService _userService;

        public CreateUserRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.CreateAsync(new User { 
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                OrganizationId = request.OrganizationId
            });
            return userId;
        }
    }
}
