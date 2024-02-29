using CommonLib.DTO;
using CommonLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    internal class InhabitantRunner : UserBase, IUserStrategy
    {
        public InhabitantRunner(UserDTO user) : base(user)
        {
        }
        //To do реализовать
        public void RunUser()
        {
            Console.WriteLine("Запуск с правами жителя");
        }
    }
}
