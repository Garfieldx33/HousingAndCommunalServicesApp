using CommonLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy
{
    public interface IStrategy<T>
    {
        public Task<T?> GetResponce(string uri, string? content);
    }
}
