using CommonLib.Enums;
using UiConsole.Strategy.StrategyImpl;
using UiConsole.Strategy;

namespace UiConsole
{
    public static class CommonMethodsInvoker
    {
       public static async Task<T?> GetInfoFromWebAPI<T>(string uri, HttpMethodsEnum method, string content) where T : class
        {
            return method switch
            {
                HttpMethodsEnum.Get => await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content),
                HttpMethodsEnum.Post => await new Requester<T?>(new PostRequester<T?>()).GetRequestResult(uri, content),
                HttpMethodsEnum.Put => await new Requester<T?>(new PutRequester<T?>()).GetRequestResult(uri, content),
                HttpMethodsEnum.Delete => await new Requester<T?>(new GetRequester<T?>()).GetRequestResult(uri, content),
                _ => null,
            };
        }
    }
}
