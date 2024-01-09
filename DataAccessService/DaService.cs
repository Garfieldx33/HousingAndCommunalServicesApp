
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
                Console.WriteLine("�������� ����������� ���� ������...");
                if (context.Database.CanConnect())
                {
                    Console.WriteLine("���� ������ ��������");
                    context.Apps.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.AppStatus.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.AppTypes.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.Departaments.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.Users.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                    context.UserTypes.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                }
                else
                {
                    Console.WriteLine("���� ������ ����������");
                }
            }
            Console.ReadKey();
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("�����");
            return Task.CompletedTask;
        }
    }
}
