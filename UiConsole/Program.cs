// See https://aka.ms/new-console-template for more information

using CommonLib.DTO;
using CommonLib.Enums;
using Newtonsoft.Json;
using UiConsole.Strategy;
using UiConsole.Strategy.StrategyImpl;

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
        switch ((UserTypeEnum)userInfo.UserTypeId)
        {
            case UserTypeEnum.Administrator:
                Console.WriteLine();
                break;
            case UserTypeEnum.Employee:
                Console.WriteLine();
                break;
            case UserTypeEnum.Inhabitant:
                Console.WriteLine();
                break;
            default: 
                Console.WriteLine("У введенного пользователя отсутствует права на взаимодействие");
                break;
        }
    }
}


Console.ReadKey();



static async Task<T?> GetInfoFromWebAPI<T>(string uri, HttpMethodsEnum method, string content) where T : class
{
    switch (method)
    {
        case HttpMethodsEnum.Get:
            return await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content);
        case HttpMethodsEnum.Post:
            return await new Requester<T?>(new PostRequester<T?>()).GetRequestResult(uri, content);
        case HttpMethodsEnum.Put:
            return await new Requester<T?>(new PutRequester<T?>()).GetRequestResult(uri, content);
        case HttpMethodsEnum.Delete:
            return await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content);
        default:
            return null;
    }
}