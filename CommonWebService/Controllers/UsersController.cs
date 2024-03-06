using CommonLib.DTO;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    UserServiceGrpc _usersService;
    public UsersController(UserServiceGrpc usersService)
    {
        _usersService = usersService;
    }

    /* [HttpGet]
     public async Task<string> GetUserByLogin(string login, string password)
     {
         return await _usersService.GetUserAsync(new UserDtoGrpc { Login = login , Password = password});
     }*/

    [HttpGet]
    public async Task<string> GetUserByLogin([FromBody] AuthDTO logPass)
    {
        return await _usersService.GetUserAsync(new UserDtoGrpc { Login = logPass.Login, Password = logPass.Pwd });
    }

    [HttpGet]
    public async Task<string> GetAllUsers()
    {
        return await _usersService.GetAllUsersAsync();
    }

    [HttpPost]
    public async Task<string> AddUser([FromBody] UserDTO newUser)
    {
        return await _usersService.AddUserAsync(newUser);
    }

    [HttpPatch]
    public async Task<string> UpdateUserInfo([FromBody] UserDTO updatingUserInfo)
    {
        return await _usersService.UpdateUserAsync(updatingUserInfo);
    }

    [HttpDelete]
    public async Task<string> DeleteUser([FromBody] int userId)
    {
        return await _usersService.DeleteUserAsync(userId);
    }
}
