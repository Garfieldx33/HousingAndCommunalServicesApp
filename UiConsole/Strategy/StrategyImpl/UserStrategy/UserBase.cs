using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    public abstract class UserBase
    {
        protected User _user;

        public UserBase(User user)
        {
            _user = user;
        }

        #region Методы обращения к API
        //Users 
        protected static async Task<List<User>> GetUsersAsync()
        {
            var users = new List<User>();
            var rawData = await CommonMethodsInvoker.GetInfoFromWebAPI<List<User>>("http://127.0.0.1:7001/Users/GetAllUsers", HttpMethodsEnum.Get, string.Empty);
            if (rawData is not null)
            {
                users = rawData;
            }
            return users;
        }
        protected static async Task<string?> AddUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("http://127.0.0.1:7001/Users/AddUser", HttpMethodsEnum.Post, userDtoString);
        }
        protected static async Task<UserDTO?> UpdateUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<UserDTO>("http://127.0.0.1:7001/Users/UpdateUserInfo", HttpMethodsEnum.Patch, userDtoString);
        }
        protected static async Task<string?> DeleteUserAsync(int userId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("http://127.0.0.1:7001/Users/DeleteUser", HttpMethodsEnum.Post, userId.ToString());
        }

        //Applications
        protected static async Task<List<Application>?> GetAppByApplicantId(int applicantId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>($"http://127.0.0.1:7001/Applications/GetAppByApplicantId/{applicantId}", HttpMethodsEnum.Get, applicantId.ToString());
        }
        public static async Task<Application?> UpdateApplication(UpdateAppDTO updatingApp)
        {
            string appString = JsonConvert.SerializeObject(updatingApp);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Application>("http://127.0.0.1:7001/Applications/UpdateApplication", HttpMethodsEnum.Patch, appString);
        }
        public static async Task<string?> DeleteApplication(int deletingAppId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>("http://127.0.0.1:7001/Applications/DeleteApplication", HttpMethodsEnum.Delete, deletingAppId.ToString());
        }
        protected static async Task<string?> AddNewApp(string newApplicationAsJson)
        {
            return await new PostNoJsonAnswerRequester<string>().GetResponce("http://127.0.0.1:7771/CreateNewApp/AddNewApp", newApplicationAsJson);
        }

        //Departments
        public static async Task<Department?> GetDepartmentById(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>(
                $"http://127.0.0.1:7001/Dictionary/GetDepartmentById/{departmentId}",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        public static async Task<List<Department>?> GetAllDepartments()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Department>>(
                "http://127.0.0.1:7001/Dictionary/GetAllDepartments",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        public static async Task<string?> AddDepartment(string departmentName)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "http://127.0.0.1:7001/Dictionary/AddDepartment",
                HttpMethodsEnum.Post,
                departmentName);
        }
        public static async Task<Department?> UpdateDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Department>(
                "http://127.0.0.1:7001/Dictionary/UpdateDepartment",
                HttpMethodsEnum.Patch,
                deptString);
        }
        public static async Task<string?> DeleteDepartment(Department department)
        {
            string deptString = JsonConvert.SerializeObject(department);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                $"http://127.0.0.1:7001/Dictionary/DeleteDepartment/{department.Id}",
                HttpMethodsEnum.Delete,
                deptString);
        }

        //Employers
        public static async Task<List<User>?> GetAllEmployersInDepartment(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<User>>("" +
                "http://127.0.0.1:7001/Employee/GetEmployersByDepartmentId",
                HttpMethodsEnum.Get,
                departmentId.ToString());
        }
        public static async Task<string?> GetDepartmentByEmployeeId(int employeeId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "http://127.0.0.1:7001/Employee/GetDepartmentByUserId",
                HttpMethodsEnum.Get,
                employeeId.ToString());
        }
        public static async Task<string?> AddNewEmployee(EmployeeInfo newEmployee)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "http://127.0.0.1:7001/Employee/AddEmployee",
                HttpMethodsEnum.Post,
                JsonConvert.SerializeObject(newEmployee));
        }
        public static async Task<string?> UpdateEmployee(EmployeeInfo employeeInfo)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "http://127.0.0.1:7001/Employee/UpdateEmployee",
                HttpMethodsEnum.Patch,
                JsonConvert.SerializeObject(employeeInfo));
        }
        public static async Task<string?> DeleteEmployee(int employeeId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<string>(
                "http://127.0.0.1:7001/Employee/DeleteEmployee",
                HttpMethodsEnum.Delete,
                employeeId.ToString());
        }

        //Dictionaries
        public static async Task<Dictionary<int, string>?> GetAppStatuses()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "http://127.0.0.1:7001/Dictionary/GetAppStatuses",
                HttpMethodsEnum.Post,
                string.Empty);
        }
        public static async Task<Dictionary<int, string>?> GetAppTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "http://127.0.0.1:7001/Dictionary/GetAppTypes",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        public static async Task<Dictionary<int, string>?> GetUserTypes()
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<Dictionary<int, string>>(
                "http://127.0.0.1:7001/Dictionary/GetUserTypes",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        #endregion

        #region Методы формирования объектов запросов

        protected async Task<ApplicationDTO?> CreateAppNewApplication()
        {
            try
            {
                ApplicationDTO result = new ApplicationDTO
                {
                    ApplicantId = _user.Id,
                    StatusId = (int)AppStatusEnum.PrimaryProcessing
                };

                Console.WriteLine("Доступные типы заявки");
                Dictionary<int, string> appTypes = await GetAppTypes();
                foreach (var appType in appTypes)
                {
                    Console.WriteLine($"{appType.Key} => " +
                        $"{EnumConverter.GetEnumDescription((AppTypeEnum)Enum.Parse(typeof(AppTypeEnum), appType.Key.ToString()))}");
                }
                Console.Write($"Выберите цифрой: ");
                int appTypeId = int.Parse(Console.ReadLine());
                result.ApplicationTypeId = appTypeId;

                Console.WriteLine("Доступные департаменты");
                List<Department> departments = await GetAllDepartments();
                foreach (var dept in departments)
                {
                    Console.WriteLine($"{dept.Id} => {dept.Name}");
                }
                Console.Write($"Выберите цифрой: ");

                int deptId = int.Parse(Console.ReadLine());
                result.DepartmentId = deptId;

                Console.Write($"Введите тему заявки: ");
                result.Subject = Console.ReadLine();
                Console.Write($"Опишите проблему наиболее полно: ");
                result.Description = Console.ReadLine();
                result.DateCreate = DateTime.Now;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось создать заявку. {ex.Message}");
            }
            return null;
        }
        protected async Task PrintApplications()
        {
            try
            {
                List<Application>? apps = await GetAppByApplicantId(_user.Id);
                if (apps is not null)
                {
                    foreach (var app in apps)
                    {
                        Console.WriteLine($"{app}");
                    }
                }
                else
                {
                    Console.WriteLine("У Вас еще нет зарегистрированных заявок");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заявок. {ex.Message}");
            }
        }
        protected async Task PrintOpenedApplications()
        {
            try
            {
                List<Application>? apps = await GetAppByApplicantId(_user.Id);
                if (apps is not null)
                {
                    foreach (var app in apps)
                    {
                        if (app.Status != AppStatusEnum.Completed
                            && app.Status != AppStatusEnum.Rejected
                            && app.Status != AppStatusEnum.Canceled)
                        {
                            Console.WriteLine($"{app}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("У Вас еще нет активных заявок");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заявок. {ex.Message}");
            }
        }
        protected async Task PrintCompletedApplications()
        {
            try
            {
                List<Application>? apps = await GetAppByApplicantId(_user.Id);
                if (apps is not null)
                {
                    foreach (var app in apps)
                    {
                        if (app.Status == AppStatusEnum.Completed)
                        {
                            Console.WriteLine($"{app}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("У Вас еще нет завершенныз заявок, требующих подтверждения");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заявок. {ex.Message}");
            }
        }
        protected static async Task<Application> CancelApplication(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO { DateClose = DateTime.Now, Id = applicationId, Status = (int)AppStatusEnum.Canceled});
        }
        protected static async Task<Application?> ConfirmApplication(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO
            {
                DateConfirm = DateTime.Now,
                Id = applicationId,
                Status = (int)AppStatusEnum.WorkСompletionСonfirmed
            });
        }
        protected async Task PrintFreeApplicationsOfDepartment()
        {

        }
        protected async Task PrintSelfApplicationInWork()
        {
            throw new NotImplementedException();
        }
        protected async Task<Application?> GetApplicationInWork(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO
            {
                DateClose = DateTime.Now,
                Id = applicationId,
                Status = (int)AppStatusEnum.ExecutorAppointed,
                ExecutorId = _user.Id
            });
        }
        protected static async Task<Application?> CompleteApplication(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO
            {
                DateClose = DateTime.Now,
                Id = applicationId,
                Status = (int)AppStatusEnum.Completed
            });
        }
        protected static async Task<Application?> AbortApplicationExecution(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO
            {
                DateClose = DateTime.Now,
                Id = applicationId,
                Status = (int)AppStatusEnum.PrimaryProcessing,
                ExecutorId = 0
            }); ;
        }
        #endregion
    }
}
