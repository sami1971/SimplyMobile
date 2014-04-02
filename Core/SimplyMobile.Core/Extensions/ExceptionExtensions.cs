using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public static class ExceptionExtensions
    {
        public static bool TryToStore(this Exception ex, ICrudProvider storage, object source)
        {
            try
            {
                return (storage != null && storage.Create(new ExceptionEntry(source, ex)) == 1);
            }
            catch
            {
                return false;
            }
        }

        public static void LogError(this ILogService logService, Exception ex)
        {
            if (logService != null)
            {
                logService.LogError(ex);
            }
        }
    }
}
