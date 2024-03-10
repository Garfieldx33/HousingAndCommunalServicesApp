using CommonLib.DTO;

namespace CommonLib.Interfaces;

public interface INotificationService
{
    public bool SendMessage(MessageDTO message);
}
