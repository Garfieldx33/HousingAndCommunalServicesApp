// See https://aka.ms/new-console-template for more information

using CommonLib.DTO;
using CommonLib.Enums;
using Newtonsoft.Json;
using UiConsole.Strategy;
using UiConsole.Strategy.StrategyImpl;
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
var userResult = GetInfoFromWebAPI<UserDTO>("https://127.0.0.1:7001/Users/GetUserByLogin", HttpMethodsEnum.Get, logPass);
if (userResult != null)
{
    if (userResult.Result != null)
    {
        UserDTO userInfo = userResult.Result;

        Console.WriteLine($"Добро пожаловать {userInfo.FirstName} {userInfo.Surname}");
        
        UserRunner userRunner = GetUserRunner(userInfo);
        userRunner.RunUser();
    }
}
Console.WriteLine("Для выхода нажмите любую клавишу");
Console.ReadKey();

static UserRunner GetUserRunner(UserDTO userInfo)
{
    return (UserTypeEnum)userInfo.UserTypeId switch
    {
        UserTypeEnum.Administrator => new UserRunner(new AdminRunner(userInfo)),
        UserTypeEnum.Employee => new UserRunner(new EmployeeRunner(userInfo)),
        UserTypeEnum.Inhabitant => new UserRunner(new InhabitantRunner(userInfo)),
        _ => new UserRunner(new UnknownRunner(userInfo)),
    };
}
static async Task<T?> GetInfoFromWebAPI<T>(string uri, HttpMethodsEnum method, string content) where T : class
{
    return method switch
    {
        HttpMethodsEnum.Get => await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content),
        HttpMethodsEnum.Post => await new Requester<T?>(new PostRequester<T?>()).GetRequestResult(uri, content),
        HttpMethodsEnum.Put => await new Requester<T?>(new PutRequester<T?>()).GetRequestResult(uri, content),
        HttpMethodsEnum.Delete => await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content),
        _ => null,
    };
}