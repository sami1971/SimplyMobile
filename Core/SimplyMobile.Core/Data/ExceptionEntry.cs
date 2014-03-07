using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class ExceptionEntry
    {
        public ExceptionEntry()
        {
            Time = DateTime.Now;
        }

        public ExceptionEntry(object source, Exception exception) : this()
        {
            Source = source.ToString();
            Exception = exception;
        }

        public string Source { get; set; }
        public DateTimeOffset Time { get; set; }
        public Exception Exception { get; set; }
    }
}
