using System;

namespace SimplyMobile.Core
{
    public interface IPasswordStorage
    {
        bool DeletePassword(string username, string service);
        bool SetPassword(string username, string service, string password);
        bool TryGetPassword(string username, string service, out string password);
    }
}

