// See https://aka.ms/new-console-template for more information

using CommonLib.DTO;
using CommonLib.Enums;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Http.Json;
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
var user = GetInfoFromWebAPI<UserDTO>("https://127.0.0.1:7001/Users/GetUserByLogin", HttpMethodsEnum.Get, logPass);
if (user != null )
{
    Console.WriteLine($"Добро пожаловать {user?.Result?.FirstName} {user?.Result?.Surname}");
}


Console.ReadKey();



static async Task<T?> GetInfoFromWebAPI<T>(string uri, HttpMethodsEnum method, string content)
{
    switch (method)
    {
        case HttpMethodsEnum.Get:
            return await new Requester<T>(new GetRequester<T>()).GetRequestResult(uri, content);
        case HttpMethodsEnum.Post:
        case HttpMethodsEnum.Put:
            
        case HttpMethodsEnum.Delete:
            
        default: 
            return null;
    }
}