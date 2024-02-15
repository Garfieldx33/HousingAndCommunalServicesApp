using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        UserServiceGrpc _usersService;
        public UsersController(UserServiceGrpc usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<string> GetUserByLogin(UserDtoGrpc user)
        {
            return await _usersService.GetUserAsync(user);
        }

        [HttpGet]
        public async Task<string> GetAllUsers()
        {
            return await _usersService.GetAllUsersAsync();
        }

        [HttpPost]
        public async Task<string> AddUser(UserGrpc newUser)
        {
            return await _usersService.AddUserAsync(newUser);
        }

        [HttpPatch]
        public async Task<string> UpdateUserInfo(UserGrpc updatingUserInfo)
        {
            return await _usersService.UpdateUserAsync(updatingUserInfo);
        }

        [HttpDelete]
        public async Task<string> DeleteUser(UserDtoRequest deletingUserInfo)
        {
            return await _usersService.DeleteUserAsync(deletingUserInfo);
        }
    }
}
