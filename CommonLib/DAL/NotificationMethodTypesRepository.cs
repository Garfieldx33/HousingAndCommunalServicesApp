using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<NotificationType> GetNotificationTypeByIdAsync(int departamentId, CancellationToken cancellation = default)
        {
            var query = context.NotificationTypes.AsNoTracking();
            if (departamentId > 0)
            {
                query = query.Where(q => q.Id == departamentId);
            }
            return await Task.FromResult(query.First());
        }

        public async Task<NotificationType> AddNotificationTypeAsync(string newNotificationTypeName, CancellationToken cancellation = default)
        {
            var query = context.NotificationTypes.AsNoTracking();
            context.NotificationTypes.Add(new NotificationType { Name = newNotificationTypeName });
            await context.SaveChangesAsync(cancellation);
            query = query.Where(d => d.Name == newNotificationTypeName);
            return await Task.FromResult(query.First());
        }

        public async Task<int> UpdateNotificationTypeAsync(NotificationType notificationType, CancellationToken cancellation = default)
        {

            if (notificationType.Id > 0)
            {
                NotificationType? updatedNotificationType = context.NotificationTypes.Single(q => q.Id == notificationType.Id);
                if (notificationType != null)
                {
                    updatedNotificationType.Name = notificationType.Name;
                    context.NotificationTypes.Update(updatedNotificationType);
                }
            }
            return await context.SaveChangesAsync(cancellation);
        }

        public async Task<int> DeleteNotificationTypeAsync(NotificationType notificationType, CancellationToken cancellation = default)
        {
            var query = context.NotificationTypes.AsNoTracking();
            if (notificationType.Id > 0)
            {
                context.NotificationTypes.Remove(notificationType);
            }
            return await context.SaveChangesAsync(cancellation);
        }
    }
}
