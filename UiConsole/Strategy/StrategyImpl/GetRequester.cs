using CommonLib.Enums;
using System.Net.Http.Json;

namespace UiConsole.Strategy.StrategyImpl
{
    public class GetRequester<T> : RequesterBase, IStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responceGet = await _httpClient.GetAsync(uri);
            return await responceGet.Content.ReadFromJsonAsync<T>();
        }
    }
}
