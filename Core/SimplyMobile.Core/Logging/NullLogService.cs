using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Core
{
    public class NullLogService : ILogService
    {
        public void Exception(object sender, Exception ex)
        {
        }

        public void Info(object sender, string message, params object[] parameters)
        {
        }

        public void Warning(object sender, string warning, params object[] parameters)
        {
        }
    }
}
