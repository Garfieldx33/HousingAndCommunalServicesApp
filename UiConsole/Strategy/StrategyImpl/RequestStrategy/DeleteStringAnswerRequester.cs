using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.RequestStrategy
{
    internal class DeleteStringAnswerRequester : RequesterBase, IRequestStrategy<string>
    {
        public async Task<string> GetResponce(string uri, string? content)
        {
            HttpResponseMessage responceDelete = await _httpClient.DeleteAsync(uri);
            return await responceDelete.Content.ReadAsStringAsync();
        }
    }
}
