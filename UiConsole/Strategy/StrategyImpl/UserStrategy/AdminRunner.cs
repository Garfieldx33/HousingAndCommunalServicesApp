using CommonLib.DTO;
using CommonLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    internal class AdminRunner : UserBase, IUserStrategy
    {
        public AdminRunner(UserDTO user) : base(user)
        {
        }

        //To do реализовать
        public void RunUser()
        {
            Console.WriteLine($@"Запуск с правами администратора
Здравствуйте, {_user.FirstName} {_user.Surname}
Выберите раздел: 1 - Пользователи, 2 - Заявки, 3 - Департаменты,  Q - выход");
            char s = Console.ReadKey().KeyChar;
            switch (s)
            {
                case ('1'):

                    break;
                case ('2'):
                    break;
                case ('3'):

                    break;
                case ('Q'):
                    return;
                default:
                    Console.WriteLine("Введите валидную команду");
                    break;
            }
        }


    }
}
