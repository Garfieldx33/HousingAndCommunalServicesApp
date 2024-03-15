using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PostRequester<T> : RequesterBase, IRequestStrategy<T>
    {
        public async Task<T?> GetResponce(string uri, string? content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            try
            {
                var con = new StringContent(content, Encoding.UTF8, "application/json");
                using HttpResponseMessage responcePost = await _httpClient.PostAsync(uri, con);
                return await responcePost.Content.ReadFromJsonAsync<T>(options: options);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return default;
            }
        }
    }
}
