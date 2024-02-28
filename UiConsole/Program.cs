// See https://aka.ms/new-console-template for more information

using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Http.Json;

Console.Write("Логин: ");
string login = Console.ReadLine();

Console.Write("Пароль: ");
string pwd = Console.ReadLine();

Console.WriteLine($"Добро пожаловать {login}");

Console.ReadKey();



static async Task<T?> GetInfoFromWebAPI<T>(string uri, string method, string content) where T : class
{
    var httpClient = new HttpClient();

    switch (method)
    {
        case "GET":
            HttpResponseMessage responceGet = await httpClient.GetAsync(uri);
            return await responceGet.Content.ReadFromJsonAsync<T>();
        case "POST":
            HttpResponseMessage responcePost = await httpClient.PostAsync(uri, JsonContent.Create(content));
            return await responcePost.Content.ReadFromJsonAsync<T>();
        case "PUT":
            HttpResponseMessage responcePut = await httpClient.PutAsync(uri, JsonContent.Create(content));
            return await responcePut.Content.ReadFromJsonAsync<T>();
        case "DELETE":
            HttpResponseMessage responceDelete = await httpClient.DeleteAsync(uri);
            return await responceDelete.Content.ReadFromJsonAsync<T>();
        default: 
            return null;
    }
}