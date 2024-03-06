using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{
    //todo реализация оповещений для пользователей и персонала р
    public Message GetMessagebyApplicationId(int applicationId, CancellationToken cancellation = default)
    {
        return (Message)_context.Messages.AsNoTracking().Where(u => u.ApplicationId == applicationId);
    }

    public async Task<int> AddNewMessage(Message newMessage, CancellationToken cancellation = default)
    {
        await _context.Messages.AddAsync(newMessage, cancellation);
        return await _context.SaveChangesAsync(cancellation);
    }

    public Message CreateNewMessage(User applicantInfo, NotificationType notificationType, Application applicationInfo)
    {
        return new Message
        {
            ApplicationId = applicantInfo.Id,
            Subject = applicationInfo.Subject,
            Body = applicationInfo.Description,
            MessagingMethodId = notificationType.Id,
            Destination = applicantInfo.MessagingDestination
        };
    }
}
