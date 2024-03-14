using CommonLib.DTO;
using CommonLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    internal class EmployeeRunner : UserBase, IUserStrategy
    {
        public EmployeeRunner(User user) : base(user)
        {
        }
        //To do реализовать
        public Task RunUser()
        {
            Console.WriteLine("Запуск с правами сотрудника УК");
            return Task.CompletedTask;
        }
    }
}
