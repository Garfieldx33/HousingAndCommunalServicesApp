using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Microsoft.AspNetCore.Mvc;
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

        //Users 
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

        //Applications
        protected static async GetAppByApplicantId(int applicantId)
        {

        }

        public async Task<string> UpdateApplication([FromBody] UpdateAppDTO updatingApp)
        {

        }

        public async Task<string> DeleteApplication([FromBody] DeleteAppRequest deletingApp)
        {

        }

        protected static Task AddNewApp(ApplicationDTO newApplication)
        {

        }

        //Departments
        public async Task<string> GetDepartmentById(int departmentId)
        {

        }

        public async Task<string> GetAllDepartments()
        {

        }
        public async Task<string> AddDepartment(string departmentName)
        {

        }

        public async Task<string> UpdateDepartment(Department department)
        {

        }

        public async Task<string> DeleteDepartment([FromBody] Department department)
        {

        }
        
        //Employers

        public static Task GetAllEmployers()
        {

        }
        public static Task GetEmployee()
        {

        }

        public static Task AddNewEmployee()
        {

        }

        public static Task UpdateEmployee()
        {

        }

        public static Task DeleteEmployee()
        {

        }
        //Dictionaries
        public async Task<string> GetAppStatuses()
        {

        }

        public async Task<string> GetAppTypes()
        {

        }

        public async Task<string> GetUserTypes()
        {

        }

    }
}
