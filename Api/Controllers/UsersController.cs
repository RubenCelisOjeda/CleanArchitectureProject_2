using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Auth.Users;

namespace Api.Controllers
{
    [Authorize("AdminUserPermissionsPolicy")]
    [Route("/[controller]")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<UserDetailsDto>> GetUsers()
        {
            return await Mediator.Send(new GetUsersRequest());
        }

        [HttpGet("{id}")]
        public async Task<UserDetailsDto> GetUser(int id)
        {
            return await Mediator.Send(new GetUserByIdRequest { Id = id });
        }

        [HttpPost]
        public async Task<int> CreateUser(CreateUserRequest createUserRequest)
        {
            return await Mediator.Send(createUserRequest);
        }

        [HttpPut("{id}")]
        public async Task<int> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            return await Mediator.Send(updateUserRequest);
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteUser(int id)
        {
            return await Mediator.Send(new DeleteUserRequest { Id = id });
        }
    }
}

