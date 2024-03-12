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
        /*
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
        protected static async Task<List<Application>> GetAppByApplicantId(int applicantId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>("https://127.0.0.1:7001/Applications/DeleteUser", HttpMethodsEnum.Get, applicantId.ToString());
        }

        public async Task<Application> UpdateApplication(UpdateAppDTO updatingApp)
        {
            string appString = JsonConvert.SerializeObject(updatingApp);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Application>("https://127.0.0.1:7001/Applications/UpdateApplication", HttpMethodsEnum.Put, appString);
        }

        public async Task<string> DeleteApplication(int deletingAppId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Applications/DeleteApplication", HttpMethodsEnum.Delete, deletingAppId.ToString());
        }

        protected static Task AddNewApp(ApplicationDTO newApplication)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        //Departments
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>("https://127.0.0.1:7001/Dictionary/GetDepartmentById", HttpMethodsEnum.Get, departmentId.ToString());
        }

        public async Task<string> GetAllDepartments()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Dictionary/GetAllDepartments", HttpMethodsEnum.Get, string.Empty);
        }
        public async Task<string> AddDepartment(string departmentName)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Dictionary/AddDepartment", HttpMethodsEnum.Post, departmentName);
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>("https://127.0.0.1:7001/Dictionary/UpdateDepartment", HttpMethodsEnum.Put, deptString);
        }

        public async Task<string> DeleteDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Dictionary/DeleteDepartment", HttpMethodsEnum.Delete, deptString);
        }
        
        //Employers
        //ToDo
        public static Task GetAllEmployers()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }
        public static Task GetEmployee()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        public static Task AddNewEmployee()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        public static Task UpdateEmployee()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        public static Task DeleteEmployee()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }
        
        //Dictionaries
        public async Task<Dictionary<int, string>> GetAppStatuses()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>("https://127.0.0.1:7001/Dictionary/GetAppStatuses", HttpMethodsEnum.Post, string.Empty);
        }

        public async Task<Dictionary<int, string>> GetAppTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>("https://127.0.0.1:7001/Dictionary/GetAppTypes", HttpMethodsEnum.Get, string.Empty);
        }

        public async Task<Dictionary<int, string>> GetUserTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>("https://127.0.0.1:7001/Dictionary/GetUserTypes", HttpMethodsEnum.Get, string.Empty);
        }
        */
    }
}
