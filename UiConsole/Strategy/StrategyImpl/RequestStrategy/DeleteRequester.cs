using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.RequestStrategy
{
    public class DeleteRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responceDelete = await _httpClient.DeleteAsync(uri);
            return await responceDelete.Content.ReadFromJsonAsync<T>();
        }
    }
}
