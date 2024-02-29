using CommonLib.DTO;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    public abstract class UserBase
    {
        protected UserDTO _user;

        public UserBase(UserDTO user) 
        {
            _user = user;
        }
    }
}
