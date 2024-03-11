using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Newtonsoft.Json;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    public abstract class UserBase
    {
        protected UserDTO _user;

        public UserBase(UserDTO user)
        {
            _user = user;
        }

        protected static async Task<List<User>> GetUsersAsync()
        {
            var users = new List<User>();
            var rawData = await CommonMethodsInvoker.GetInfoFromWebAPI<List<User>>("https://127.0.0.1:7001/Users/GetAllUsers", HttpMethodsEnum.Get, string.Empty);
            if (rawData is not null)
            {
                users = rawData;
            }
            return users;
        }

        protected static async Task<string> AddUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/AddUser", HttpMethodsEnum.Post, userDtoString);
        }

        protected static async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<UserDTO>("https://127.0.0.1:7001/Users/UpdateUserInfo", HttpMethodsEnum.Put, userDtoString);
        }

        protected static async Task<string> DeleteUserAsync(int userId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }
    }
}
