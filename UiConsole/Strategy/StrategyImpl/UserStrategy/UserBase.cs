using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    public abstract class UserBase
    {
        protected User _user;

        public UserBase(User user)
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
        
        protected static async Task<string?> AddUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/AddUser", HttpMethodsEnum.Post, userDtoString);
        }

        protected static async Task<UserDTO?> UpdateUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<UserDTO>("https://127.0.0.1:7001/Users/UpdateUserInfo", HttpMethodsEnum.Put, userDtoString);
        }

        protected static async Task<string?> DeleteUserAsync(int userId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        //Applications
        protected static async Task<List<Application>?> GetAppByApplicantId(int applicantId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>("https://127.0.0.1:7001/Applications/DeleteUser", HttpMethodsEnum.Get, applicantId.ToString());
        }

        public static async Task<Application?> UpdateApplication(UpdateAppDTO updatingApp)
        {
            string appString = JsonConvert.SerializeObject(updatingApp);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Application>("https://127.0.0.1:7001/Applications/UpdateApplication", HttpMethodsEnum.Put, appString);
        }

        public static async Task<string?> DeleteApplication(int deletingAppId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("https://127.0.0.1:7001/Applications/DeleteApplication", HttpMethodsEnum.Delete, deletingAppId.ToString());
        }

        protected static async Task<string?> AddNewApp(ApplicationDTO newApplication)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/CreateNewApp/AddNewApp",
                HttpMethodsEnum.Post,
                JsonConvert.SerializeObject(newApplication));
        }

        //Departments
        public static async Task<Department?> GetDepartmentById(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>(
                "https://127.0.0.1:7001/Dictionary/GetDepartmentById",
                HttpMethodsEnum.Get, 
                departmentId.ToString());
        }

        public static async Task<string?> GetAllDepartments()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Dictionary/GetAllDepartments",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        public static async Task<string?> AddDepartment(string departmentName)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Dictionary/AddDepartment",
                HttpMethodsEnum.Post,
                departmentName);
        }

        public static async Task<Department?> UpdateDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>(
                "https://127.0.0.1:7001/Dictionary/UpdateDepartment",
                HttpMethodsEnum.Put, 
                deptString);
        }

        public static async Task<string?> DeleteDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Dictionary/DeleteDepartment",
                HttpMethodsEnum.Delete, 
                deptString);
        }
        
        //Employers
        public static async Task<List<User>?> GetAllEmployersInDepartment(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<User>>("" +
                "https://127.0.0.1:7001/Employee/GetEmployersByDepartmentId", 
                HttpMethodsEnum.Get, 
                departmentId.ToString());
        }
        public static async Task<string?> GetDepartmentByEmployeeId(int employeeId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Employee/GetDepartmentByUserId", 
                HttpMethodsEnum.Get, 
                employeeId.ToString());
        }

        public static async Task<string?> AddNewEmployee(EmployeeInfo newEmployee)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Employee/AddEmployee",
                HttpMethodsEnum.Post,
                JsonConvert.SerializeObject(newEmployee));
        }

        public static async Task<string?> UpdateEmployee(EmployeeInfo employeeInfo)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Employee/UpdateEmployee", 
                HttpMethodsEnum.Put,
                JsonConvert.SerializeObject(employeeInfo));
        }

        public static async Task<string?> DeleteEmployee(int employeeId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "https://127.0.0.1:7001/Employee/DeleteEmployee",
                HttpMethodsEnum.Delete,
                employeeId.ToString());
        }
        
        //Dictionaries
        public static async Task<Dictionary<int, string>?> GetAppStatuses()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "https://127.0.0.1:7001/Dictionary/GetAppStatuses", 
                HttpMethodsEnum.Post,
                string.Empty);
        }

        public static async Task<Dictionary<int, string>?> GetAppTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "https://127.0.0.1:7001/Dictionary/GetAppTypes",
                HttpMethodsEnum.Get, 
                string.Empty);
        }

        public static async Task<Dictionary<int, string>?> GetUserTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "https://127.0.0.1:7001/Dictionary/GetUserTypes",
                HttpMethodsEnum.Get,
                string.Empty);
        }
    }
}
