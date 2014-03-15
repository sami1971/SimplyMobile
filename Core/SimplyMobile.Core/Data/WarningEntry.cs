using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public class WarningEntry : InfoEntry
    {
        public WarningEntry() { }
        public WarningEntry(object sender, string message, params object[] parameters) : base(sender, message, parameters) { }
    }
}
