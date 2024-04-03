// See https://aka.ms/new-console-template for more information

using CommonLib.Entities;
using CommonLib.Enums;
using UiConsole;
using UiConsole.Strategy;
using UiConsole.Strategy.StrategyImpl.UserStrategy;
Mutex mutex = new(true, "UiConsole", out bool createdNew);
if (createdNew)
{
    mutex.WaitOne();
    Console.Write("Логин: ");
    string? login = Console.ReadLine();
    Console.Write("Пароль: ");
    string? pwd = Console.ReadLine();

    var userResult = await CommonMethodsInvoker.GetInfoFromWebAPI<User>(
        $@"http://192.168.50.175:7080/Users/GetUserByLoginPwd/{login}/{pwd}", HttpMethodsEnum.Get, null);
    if (userResult != null)
    {
        User userInfo = userResult;
        Console.WriteLine($"Добро пожаловать {userInfo.FirstName} {userInfo.SecondName}");
        UserRunner userRunner = GetUserRunner(userInfo);
        userRunner.RunUser();

    }
    Console.WriteLine("Для выхода нажмите любую клавишу");
    Console.ReadKey();
    mutex.ReleaseMutex();
}
else
{
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine($"Невозможно запустить второй экземпляр приложения. Автоматический выход через 5 секунд");
    Thread.Sleep(5000);
    mutex.Dispose();
}


static UserRunner GetUserRunner(User userInfo)
{
    return userInfo.TypeId switch
    {
        UserTypeEnum.Administrator => new UserRunner(new AdminRunner(userInfo)),
        UserTypeEnum.Employee => new UserRunner(new EmployeeRunner(userInfo)),
        UserTypeEnum.Inhabitant => new UserRunner(new InhabitantRunner(userInfo)),
        _ => new UserRunner(new UnknownRunner(userInfo)),
    };
}
