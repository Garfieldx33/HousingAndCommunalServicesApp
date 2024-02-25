using CommonLib.DTO;
using CommonLib.Entities;
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
        public async Task<string> GetUserByLogin(string login, string password)
        {
            return await _usersService.GetUserAsync(new UserDtoGrpc { Login = login , Password = password});
        }

        [HttpGet]
        public async Task<string> GetAllUsers()
        {
            return await _usersService.GetAllUsersAsync();
        }

        [HttpPost]
        public async Task<string> AddUser([FromQuery] UserDTO newUser)
        {
            return await _usersService.AddUserAsync(newUser);
        }

        [HttpPatch]
        public async Task<string> UpdateUserInfo([FromQuery] UserDTO updatingUserInfo)
        {
            return await _usersService.UpdateUserAsync(updatingUserInfo);
        }

        [HttpDelete]
        public async Task<string> DeleteUser([FromQuery] int userId)
        {
            return await _usersService.DeleteUserAsync(userId);
        }
    }
}
