using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy
{
    internal class InhabitantRunner : UserBase, IUserStrategy
    {
        public InhabitantRunner(User user) : base(user)
        {
        }
        public async Task RunUser()
        {
            Console.WriteLine("Запуск с правами жителя");
            Console.WriteLine($@"Здравствуйте, {_user.FirstName}{_user.SecondName}.
            Выберите действие: 1 - Подать заявку, 2 - Просмотреть Ваши заявки, 3 - Отменить заявку,  Q - выход");

            char s = Console.ReadKey().KeyChar;
            while (s != 'Q')
            {
                switch (s)
                {
                    case ('1'):
                        ApplicationDTO? newApp = await CreateAppNewApplication();
                        if (newApp != null)
                        {
                            string jsonApp = JsonConvert.SerializeObject(newApp);
                            Console.WriteLine(jsonApp);
                            var addingResult = await AddNewApp(jsonApp);
                            Console.WriteLine(addingResult);
                        }
                        break;
                    case ('2'):
                        await PrintApplications();
                        break;
                    case ('3'):
                        Console.WriteLine("Выберите заявку из списка. Если заявка не найдена, то введите 0");
                        await PrintOpenedApplications();
                        int delAppId = int.Parse(Console.ReadLine());
                        if (delAppId != 0)
                        {
                            var canceledApp = await CancelApplication(delAppId);
                            if (canceledApp is not null)
                            {
                                Console.WriteLine($"Заявка с ID {canceledApp.Id} в статусе {EnumConverter.GetEnumDescription((AppStatusEnum)Enum.Parse(typeof(AppStatusEnum), canceledApp.Status.ToString()))}");
                            }
                        }
                        break;
                    case ('Q'):
                        return;
                    default:
                        Console.WriteLine("Введите валидную команду");
                        break;
                }
                Console.WriteLine("Выберите действие: 1 - Подать заявку, 2 - Просмотреть Ваши заявки, 3 - Отменить заявку,  Q - выход =>");
                s = Console.ReadKey().KeyChar;
            }
            Console.ReadKey();
        }
    }
}
