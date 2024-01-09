
using CommonLib.DAL;

namespace DataAccessService
{
    public class DaService : IHostedService
    {
        public DaService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext())
            {
                Console.WriteLine("Проверка доступности базы данных...");
                if (context.Database.CanConnect())
                {
                    Console.WriteLine("База данных доступна");
                    context.Apps.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.AppStatus.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.AppTypes.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.Departaments.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.Users.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.UserTypes.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                }
                else
                {
                    Console.WriteLine("База данных недоступна");
                }
            }
            Console.ReadKey();
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Выход");
            return Task.CompletedTask;
        }
    }
}
