using Newtonsoft.Json;
using System.Text;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PostStringAnswerRequester<T> : RequesterBase, IRequestStrategy<string>
    {
        public async Task<string> GetResponce(string uri, string? content)
        {
            var con = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responcePut = await _httpClient.PostAsync(uri, con);
            return await responcePut.Content.ReadAsStringAsync();
        }
    }
}
