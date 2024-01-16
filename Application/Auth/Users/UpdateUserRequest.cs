using Application.Common.Exceptions;

using Domain;

namespace Application.Auth.Users
{
    public class UpdateUserRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OrganizationId { get; set; }

    }

    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, int>
    {
        private readonly IUserService _userService;

        public UpdateUserRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userService.UpdateAsync(new User {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                OrganizationId = request.OrganizationId
            });
            return request.Id;
        }
    }
}
