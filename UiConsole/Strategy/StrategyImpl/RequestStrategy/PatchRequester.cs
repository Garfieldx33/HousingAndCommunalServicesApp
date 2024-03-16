using CommonLib.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PatchRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            var con = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responcePut = await _httpClient.PatchAsync(uri, con);
            var res =  await responcePut.Content.ReadAsStringAsync();
            T r = JsonConvert.DeserializeObject<T>(res);
            return r;
        }
    }
}
