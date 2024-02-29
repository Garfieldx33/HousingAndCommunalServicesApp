using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy
{
    public class UserRunner
    {
        IUserStrategy UserRunnerStrategy;

        public UserRunner(IUserStrategy userRunnerStrategy)
        {
            UserRunnerStrategy = userRunnerStrategy;
        }   

        public void RunUser()
        {
            UserRunnerStrategy?.RunUser();
        }
    }
}
