using CommonLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    internal class UnknownRunner : UserBase, IUserStrategy
    {
        public UnknownRunner(UserDTO user) : base(user)
        {
        }

        public void RunUser()
        {
            Console.WriteLine($"Получите права в кабинете номер 101");
        }
    }
}
