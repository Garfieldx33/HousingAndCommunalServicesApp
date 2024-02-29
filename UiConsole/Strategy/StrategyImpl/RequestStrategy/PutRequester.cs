using CommonLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PutRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responcePut = await _httpClient.PutAsync(uri, JsonContent.Create(content));
            return await responcePut.Content.ReadFromJsonAsync<T>();
        }
    }
}
