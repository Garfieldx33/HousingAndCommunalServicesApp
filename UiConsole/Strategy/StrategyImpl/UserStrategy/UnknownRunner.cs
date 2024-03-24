using CommonLib.Entities;

namespace UiConsole.Strategy.StrategyImpl.UserStrategy;

internal class UnknownRunner : UserBase, IUserStrategy
{
    public UnknownRunner(User user) : base(user)
    {
    }

    public Task RunUser()
    {
        Console.WriteLine($"Получите права в кабинете номер 101");
        Console.WriteLine("До свидания");
        return Task.CompletedTask;
    }
}
