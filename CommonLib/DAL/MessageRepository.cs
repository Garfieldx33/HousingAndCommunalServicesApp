using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL;

public partial class PostgresRepository
{
    public Message GetMessagebyApplicationId(int applicationId, CancellationToken cancellation = default)
    {
        return (Message)_context.Messages.AsNoTracking().Where(u => u.ApplicationId == applicationId);
    }

    public async Task<int> AddNewMessage(Message newMessage, CancellationToken cancellation = default)
    {
        await _context.Messages.AddAsync(newMessage, cancellation);
        return await _context.SaveChangesAsync(cancellation);
    }
}
