﻿using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using UiConsole.Strategy.StrategyImpl.RequestStrategy;

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
            return await new PostStringAnswerRequester<string>().GetResponce("http://127.0.0.1:7001/Users/AddUser", userDtoString);
        }
        protected static async Task<UserDTO?> UpdateUserAsync(UserDTO userDTO)
        {
            string userDtoString = JsonConvert.SerializeObject(userDTO);
            return await CommonMethodsInvoker.GetInfoFromWebAPI<UserDTO>("http://127.0.0.1:7001/Users/UpdateUserInfo", HttpMethodsEnum.Patch, userDtoString);
        }
        protected static async Task<string?> DeleteUserAsync(int userId)
        {
            return await new DeleteStringAnswerRequester().GetResponce($"http://127.0.0.1:7001/Users/DeleteUser/{userId}", string.Empty);
        }

        //Applications
        protected static async Task<List<Application>?> GetAppByApplicantId(int applicantId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>($"http://127.0.0.1:7001/Applications/GetAppByApplicantId/{applicantId}", HttpMethodsEnum.Get, applicantId.ToString());
        }
        protected static async Task<List<Application>?> GetOpenedAppByDepartmentId(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>($"http://127.0.0.1:7001/Applications/GetOpenedAppByDepartmentId/{departmentId}", HttpMethodsEnum.Get, departmentId.ToString());
        }
        protected static async Task<List<Application>?> GetAppByExecutorId(int executorId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<Application>>($"http://127.0.0.1:7001/Applications/GetAppByExecutorId/{executorId}", HttpMethodsEnum.Get, executorId.ToString());
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
            return await new PostStringAnswerRequester<string>().GetResponce("http://127.0.0.1:7771/CreateNewApp/AddNewApp", newApplicationAsJson);
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
            return await new DeleteStringAnswerRequester().GetResponce(
                $"http://127.0.0.1:7001/Dictionary/DeleteDepartment/{department.Id}",
                deptString);
        }

        //Employers
        public static async Task<List<User>?> GetAllEmployersInDepartment(int departmentId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<List<User>?>("" +
                $"http://127.0.0.1:7001/Employee/GetEmployersByDepartmentId/{departmentId}",
                HttpMethodsEnum.Get,
                departmentId.ToString());
        }
        public static async Task<string?> GetDepartmentByEmployeeId(int employeeId)
        {
            return await new GetStringAnswerRequester<string>().GetResponce(
                $"http://127.0.0.1:7001/Employee/GetDepartmentByUserId/{employeeId}", string.Empty);
        }
        public static async Task<EmployeeInfo?> GetEmployeeInfoByUserUd(int employeeId)
        {
            return await CommonMethodsInvoker.GetInfoFromWebAPI<EmployeeInfo>(
                $"http://127.0.0.1:7001/Employee/GetEmployeeInfoByUserId/{employeeId}",
                HttpMethodsEnum.Get,
                string.Empty);
        }
        public static async Task<string?> AddNewEmployee(EmployeeInfo newEmployee)
        {
            return await new PostStringAnswerRequester<string>().GetResponce(
                "http://127.0.0.1:7001/Employee/AddEmployee",
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
            return await new DeleteStringAnswerRequester().GetResponce(
                $"http://127.0.0.1:7001/Employee/DeleteEmployee/{employeeId}",
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

        protected async Task PrintFreeApplicationsOfDepartment()
        {
            try
            {
                var depts = await GetAllDepartments();
                if (depts is not null)
                {
                    string? deptName = await GetDepartmentByEmployeeId(_user.Id);
                    int deptId = depts.Where(u => u.Name == deptName).Select(i => i.Id).First();
                    List<Application>? apps = await GetOpenedAppByDepartmentId(deptId);
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

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заявок. {ex.Message}");
            }
        }
        protected async Task PrintSelfApplicationInWork()
        {
            try
            {
                var apps = await GetAppByExecutorId(_user.Id);
                if (apps is not null)
                {
                    foreach (var app in apps)
                    {
                        Console.WriteLine($"{app}");
                    }
                }
                else
                {
                    Console.WriteLine("У Вас еще нет взятых в работу заявок");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка заявок. {ex.Message}");
            }
        }
        protected async Task<Application?> GetApplicationInWork(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO
            {
                Id = applicationId,
                Status = (int)AppStatusEnum.ExecutorAppointed,
                ExecutorId = _user.Id
            });
        }
        protected async Task<Application?> CompleteApplication(int applicationId)
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
                Id = applicationId,
                Status = (int)AppStatusEnum.PrimaryProcessing,
                ExecutorId = -1
            }); ;
        }
        protected static async Task<Application> CancelApplication(int applicationId)
        {
            return await UpdateApplication(new UpdateAppDTO { DateClose = DateTime.Now, Id = applicationId, Status = (int)AppStatusEnum.Canceled });
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

        // TODO
        protected void ProcessApplications()
        {
            Console.WriteLine(@"Выберите действие:
1 - Поиск заявки по номеру
2 - Добавить заявку
3 - Изменить статус заявки
4 - Удалить заявку
Q - Выход");
            char choise = Console.ReadKey().KeyChar;

            while (choise != 'Q')
            {
                switch (choise)
                {
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case 'Q':
                        break;
                }
            }
        }

        // TODO
        protected async Task ProcessUsers()
        {
            Console.WriteLine(@"Выберите действие:
1 - Просмотреть всех пользователей
2 - Добавить пользователя
3 - Изменить пользователя
4 - Удалить пользователя
Q - Выход");
            char choise = Console.ReadKey().KeyChar;

            while (choise != 'Q')
            {
                switch (choise)
                {
                    case '1':
                        break;
                    case '2':
                        Console.WriteLine("Введите имя");
                        string fName = Console.ReadLine();

                        Console.WriteLine("Введите фамилию");
                        string sName = Console.ReadLine();
                        int rnd = new Random(999).Next(0, 988);
                        var insertResult = await AddUserAsync(new UserDTO 
                        { 
                            FirstName = fName,
                            Surname = sName,
                            UserTypeId = (int)UserTypeEnum.Inhabitant,
                            Address = $"улица Победы, дом 2, кв.{rnd}",
                            Balance = 0,
                            DateOfBirth = new DateTime(1970,5,3),
                            Email = $"newuser_{rnd}@mail.ru",
                            Login = $"NewUser_{rnd}",
                            Password = rnd.ToString(),
                            Phone = $"+78569851{rnd}"
                        });
                        Console.WriteLine(insertResult);
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case 'Q':
                        break;
                }
                choise = Console.ReadKey().KeyChar;
            }

        }

        // TODO
        protected void ProcessDepartments()
        {
            Console.WriteLine(@"Выберите действие:
1 - Просмотреть все департаменты
2 - Добавить департамент
3 - Изменить департамент
4 - Удалить департамент
Q - Выход");
            char choise = Console.ReadKey().KeyChar;

            while (choise != 'Q')
            {
                switch (choise)
                {
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case 'Q':
                        break;
                }
            }
        }

        protected async Task ProcessEmployers()
        {
            Console.WriteLine(@"Выберите действие:
1 - Просмотреть работников 
2 - Добавить работника в департамент
3 - Изменить должность работника
4 - Удалить работника из департамента
Q - Выход");
            char choise = Console.ReadKey().KeyChar;

            while (choise != 'Q')
            {
                switch (choise)
                {
                    case '1':
                        List<Department?> deptList = await GetAllDepartments();
                        deptList?.ForEach(async d =>
                        {
                            List<User>? users = await GetAllEmployersInDepartment(d.Id);
                            users?.ForEach(async user =>
                            {
                                EmployeeInfo? employeeInfo = await GetEmployeeInfoByUserUd(user.Id);
                                if (employeeInfo is not null)
                                {
                                    Console.WriteLine($"Департамент {d.Name} => {user.FirstName} {user.SecondName} => {employeeInfo.Position}");
                                }
                            });
                        });
                        break;
                    case '2':
                        Console.WriteLine("Выберите департамент");
                        List<Department?> dList = await GetAllDepartments();
                        dList?.ForEach(d => Console.WriteLine($"{d.Id} => {d.Name}"));
                        int dId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Выберите пользователя");
                        List<User> uList = await GetUsersAsync();
                        uList?.Where(u => u.TypeId != UserTypeEnum.Employee).ToList().ForEach(u => Console.WriteLine($"{u.Id} => {u.FirstName} {u.SecondName}"));
                        int uId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Введите должность");
                        string position = Console.ReadLine();
                        var result = await AddNewEmployee(new EmployeeInfo { DepartmentId = dId, EmployeeUserId = uId, Position = position});
                        Console.WriteLine(result);
                        break;
                    case '3':
                        Console.WriteLine("Введите Id пользователя");
                        int usId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите должность");
                        string pos = Console.ReadLine();
                        var updResult = UpdateEmployee(new EmployeeInfo { EmployeeUserId = usId, Position = pos});
                        Console.WriteLine(updResult);
                        break;
                    case '4':
                        Console.WriteLine("Введите Id работника");
                        int deleteUserId = int.Parse(Console.ReadLine());
                        var deleteResult = await DeleteEmployee(deleteUserId);
                        break;
                    case 'Q':
                        break;
                    default:
                        break;
                }
                choise = Console.ReadKey().KeyChar;
            }
        }
        #endregion
    }
}
