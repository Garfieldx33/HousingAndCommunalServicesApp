using CommonLib.DTO;
using CommonLib.Entities;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<UserDTO> GetUserByLoginPwd([FromBody] AuthDTO logPass)
    {
        return await _usersService.GetUserAsync(new UserDtoGrpc { Login = logPass.Login, Password = logPass.Pwd });
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await _usersService.GetAllUsersAsync();
    }

    [HttpPost]
    public async Task<string> AddUser([FromBody] UserDTO newUser)
    {
        var result = await _usersService.AddUserAsync(newUser);
        return JsonConvert.SerializeObject(result); ;
    }

    [HttpPatch]
    public async Task<UserDTO> UpdateUserInfo([FromBody] UserDTO updatingUserInfo)
    {
        return await _usersService.UpdateUserAsync(updatingUserInfo);
    }

    [HttpDelete]
    public async Task<string> DeleteUser([FromBody] int userId)
    {
        var result = await _usersService.DeleteUserAsync(userId);
        return JsonConvert.SerializeObject(result);
    }
}
