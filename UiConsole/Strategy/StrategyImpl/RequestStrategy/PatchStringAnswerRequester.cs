using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.RequestStrategy
{
    public class PatchStringAnswerRequester : RequesterBase, IRequestStrategy<string>
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
