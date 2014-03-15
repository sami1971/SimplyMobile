using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class InfoEntry
    {
        public InfoEntry()
        {
        }

        public InfoEntry(object sender, string message, params object[] parameters)
        {
            this.Sender = sender.ToString();
            this.Message = string.Format(message, parameters);
            this.TimeStamp = DateTime.Now;
        }

        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
