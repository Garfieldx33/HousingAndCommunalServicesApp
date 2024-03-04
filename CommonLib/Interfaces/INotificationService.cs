using CommonLib.Config;
using CommonLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface INotificationService
    {
        public void SendMessage(MessageDTO message);
    }
}
