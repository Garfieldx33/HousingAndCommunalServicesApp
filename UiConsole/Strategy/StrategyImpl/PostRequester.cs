using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PostRequester<T> : RequesterBase, IStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responcePost = await _httpClient.PostAsync(uri, JsonContent.Create(content));
            return await responcePost.Content.ReadFromJsonAsync<T>();
        }
    }
}
