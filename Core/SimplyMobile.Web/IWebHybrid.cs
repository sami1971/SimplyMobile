using System;

namespace SimplyMobile.Web
{
    public interface IWebHybrid : IDisposable
    {
        void InjectJavaScript(string script);

        void RegisterCallback(string name, Action<string> action);

        bool RemoveCallback(string name);

        void CallJsFunction(string function, params object[] parameters);

        void LoadFromFile(string fileName);

    }
}

