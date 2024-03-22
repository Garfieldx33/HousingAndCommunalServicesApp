using System.Text;

namespace UiConsole.Strategy.StrategyImpl
{
    public class PostStringAnswerRequester<T> : RequesterBase, IRequestStrategy<string>
    {
        public async Task<string> GetResponce(string uri, string? content)
        {

            var con = new StringContent(content, Encoding.UTF8, "application/json");
            using HttpResponseMessage responcePost = await _httpClient.PostAsync(uri, con);
            {
                return responcePost.StatusCode == System.Net.HttpStatusCode.OK ?
                    "Заявка отправлена на обработку" : "Ошибка при регистрации заявки, попробуйте позже";
            }
        }
    }
}
