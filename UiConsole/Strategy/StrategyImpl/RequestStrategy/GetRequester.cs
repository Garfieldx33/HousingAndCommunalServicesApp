﻿using Newtonsoft.Json;

namespace UiConsole.Strategy.StrategyImpl
{
    public class GetRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responceGet = await _httpClient.GetAsync(uri);
            responceGet.EnsureSuccessStatusCode();
             string data = await responceGet.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
