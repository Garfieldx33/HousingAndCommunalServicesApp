using CommonLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy
{
    public class Requester<T>
    {
        IStrategy<T> RequestingActor { get; set; }

        public Requester(IStrategy<T> requestingActor)
        {
            RequestingActor = requestingActor;
        }

        public async Task<T?> GetRequestResult(string uri, string? content)
        {
           return await RequestingActor.GetResponce(uri, content);
        }
    }
}
