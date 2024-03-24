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
        const string selectStr =
@"Выберите действие: 
1 - Пользователи 
2 - Заявки
3 - Департаменты
4 - Работники
Q - Выход";
        public AdminRunner(User user) : base(user)
        {
        }

        //To do проверить
        public async Task RunUser()
        {
            Console.WriteLine($@"Запуск с правами администратора");
            Console.WriteLine(selectStr);
            char s = Console.ReadKey().KeyChar;
            switch (s)
            {
                case ('1'):
                    await ProcessUsers();
                    break;
                case ('2'):
                    await ProcessApplications();
                    break;
                case ('3'):
                    await ProcessDepartments();
                    break;
                case ('4'):
                    await ProcessEmployers();
                    break;
                case ('Q'):
                    Console.WriteLine("До свидания");
                    return;
                default:
                    Console.WriteLine("Введите валидную команду");
                    break;
            }

        }
    }
}
