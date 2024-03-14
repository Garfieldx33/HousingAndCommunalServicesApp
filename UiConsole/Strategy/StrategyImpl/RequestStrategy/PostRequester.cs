using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PostRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            try
            {
                var cont = JsonContent.Create(content);
                //cont.Headers.Add("Content-Type", "application/json");
                using HttpResponseMessage responcePost = await _httpClient.PostAsync(uri, cont);
                return await responcePost.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return default;
            }
        }
    }
}
