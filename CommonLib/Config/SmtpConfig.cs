using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Config
{
    public class SmtpConfig
    {
        public string SmtpServerAddress { get; set; } 
        public int SmtpPort { get; set; }
        public string SourceEmailAddress { get; set; }
        public string SourceEmailPwd { get; set; }
    }
}
