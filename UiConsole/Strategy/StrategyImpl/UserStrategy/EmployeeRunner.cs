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
        public EmployeeRunner(UserDTO user) : base(user)
        {
        }

        public void RunUser()
        {
            throw new NotImplementedException();
        }
    }
}
