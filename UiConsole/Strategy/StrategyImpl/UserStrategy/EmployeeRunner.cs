using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy;

internal class EmployeeRunner : UserBase, IUserStrategy
{
    const string selectStr = 
@"Выберите действие: 
1 - Просмотреть свободные заявки 
2 - Просмотреть свои заявки
3 - Взять заявку в работу
4 - Завершить заявку
5 - Отменить исполнения своей заявки
Q - Выход";
    public EmployeeRunner(User user) : base(user)
    {
    }
    //To do реализовать
    public async Task RunUser()
    {
        Console.WriteLine("Запуск с правами сотрудника УК");
        Console.WriteLine(selectStr);

        char s = Console.ReadKey().KeyChar;
        while (s != 'Q')
        {
            switch (s)
            {
                case ('1'):
                    await PrintFreeApplicationsOfDepartment();
                    break;
                case ('2'):
                    await PrintSelfApplicationInWork();
                    break;
                case ('3'):
                    await PrintFreeApplicationsOfDepartment();
                    Console.WriteLine($"Введите идентификатор заявки, которую хотите взять в работу");
                    int getAppId = int.Parse(Console.ReadLine());
                    await GetApplicationInWork(getAppId);
                    break;
                case ('4'):
                    await PrintSelfApplicationInWork();
                    Console.WriteLine($"Введите идентификатор заявки, которую хотите завершить");
                    int completedAppId = int.Parse(Console.ReadLine().ToString());
                    await CompleteApplication(completedAppId);
                    break;
                case ('5'):
                    await PrintSelfApplicationInWork();
                    Console.WriteLine($"Введите идентификатор заявки, которую хотите прекратить выполнять");
                    int abortAppppId = int.Parse(Console.ReadLine().ToString());
                    await AbortApplicationExecution(abortAppppId);
                    break;
                case ('Q'):
                    return;
                default:
                    Console.WriteLine("Введите валидную команду");
                    break;
            }
            Console.WriteLine(selectStr);
            s = Console.ReadKey().KeyChar;
        }
        Console.ReadKey();
    }





}
