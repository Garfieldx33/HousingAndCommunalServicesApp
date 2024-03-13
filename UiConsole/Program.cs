// See https://aka.ms/new-console-template for more information

using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Newtonsoft.Json;
using UiConsole;
using UiConsole.Strategy;
using UiConsole.Strategy.StrategyImpl.UserStrategy;

Console.Write("Логин: ");
string? login = Console.ReadLine();
Console.Write("Пароль: ");
string? pwd = Console.ReadLine();
string logPass = JsonConvert.SerializeObject(
    new AuthDTO
    {
        Login = login != null ? login : string.Empty,
        Pwd = pwd != null ? pwd : string.Empty
    });
var userResult = await CommonMethodsInvoker.GetInfoFromWebAPI<User>($"http://127.0.0.1:7000/Users/GetUserByLoginPwd/{login}/{pwd}", HttpMethodsEnum.Get, logPass);
if (userResult != null)
{
    User userInfo = userResult;
    Console.WriteLine($"Добро пожаловать {userInfo.FirstName} {userInfo.SecondName}");
    UserRunner userRunner = GetUserRunner(userInfo);
    userRunner.RunUser();

}
Console.WriteLine("Для выхода нажмите любую клавишу");
Console.ReadKey();

static UserRunner GetUserRunner(User userInfo)
{
    return userInfo.TypeId switch
    {
        UserTypeEnum.Administrator => new UserRunner(new AdminRunner(userInfo)),
        UserTypeEnum.Employee => new UserRunner(new EmployeeRunner(userInfo)),
        UserTypeEnum.Inhabitant => new UserRunner(new InhabitantRunner(userInfo)),
        _ => new UserRunner(new UnknownRunner(userInfo)),
    };
}
