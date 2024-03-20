using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.RequestStrategy
{
    public class GetStringAnswerRequester : RequesterBase, IRequestStrategy<string>
    {
        public async Task<string> GetResponce(string uri, string? content)
        {

            using HttpResponseMessage responceGet = await _httpClient.GetAsync(uri);
            {
                responceGet.EnsureSuccessStatusCode();
                return await responceGet.Content.ReadAsStringAsync();
            }
        }
    }
}
