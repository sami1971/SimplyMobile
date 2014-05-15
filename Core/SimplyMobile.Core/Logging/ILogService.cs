using System;

namespace SimplyMobile.Core
{
    public interface ILogService
    {
        void Exception(object sender, Exception ex);
        void Info(object sender, string message, params object[] parameters);
        void Warning(object sender, string warning, params object[] parameters);
    }
}

