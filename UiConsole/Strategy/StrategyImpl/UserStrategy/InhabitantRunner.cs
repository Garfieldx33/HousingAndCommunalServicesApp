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
        public InhabitantRunner(User user) : base(user)
        {
        }
        //To do реализовать
        public void RunUser()
        {
            Console.WriteLine("Запуск с правами жителя");
            Console.WriteLine($@"Здравствуйте, { _user.FirstName}{ _user.SecondName}.
            Выберите действие: 1 - Подать заявку, 2 - Просмотреть статус заявки, 3 - Отменить заявку,  Q - выход");
            char s = Console.ReadKey().KeyChar;
            switch (s)
            {
                case ('1'):
                    ApplicationDTO na = new ApplicationDTO { 
                        ApplicantId = _user.Id, 
                        ApplicationTypeId = 1, 
                        DepartmentId = 6, 
                        DateCreate = DateTime.Now, 
                        StatusId = 1, 
                        Description= "Надо переехать с одного места на другое",
                        Subject = "Необходим перезд"
                    };
                    
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
