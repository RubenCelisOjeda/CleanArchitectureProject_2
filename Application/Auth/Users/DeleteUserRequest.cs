using Application.Common.Exceptions;

namespace Application.Auth.Users
{
    public class DeleteUserRequest : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, int>
    {
        private readonly IUserService _userService;

        public DeleteUserRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userService.DeleteAsync(user.Id);
            return request.Id;
        }
    }
}
