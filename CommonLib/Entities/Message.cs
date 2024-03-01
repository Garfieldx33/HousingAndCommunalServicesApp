using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessagingMethodId { get; set; }
        public string Destination { get; set; }
        public int ApplicationId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
