using CommonLib.DTO;

namespace CommonLib.Interfaces;

public interface INotificationService
{
    public void SendMessage(MessageDTO message);
}
