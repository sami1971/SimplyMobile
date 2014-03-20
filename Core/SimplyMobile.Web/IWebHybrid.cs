using System;

namespace SimplyMobile.Web
{
    public interface IWebHybrid : IDisposable
    {
        void InjectJavaScript(string script);

        void RegisterCallback(string name, Action<string> action);

        void CallJsFunction(string function, params object[] parameters);
    }
}

