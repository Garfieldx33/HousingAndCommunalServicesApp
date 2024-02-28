using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl
{
    public abstract class RequesterBase
    {
        protected HttpClient _httpClient;
        public RequesterBase()
        {
            _httpClient = new HttpClient();
        }
    }
}
