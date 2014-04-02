using System;
using System.Collections.Generic;
using System.Linq;
#if !WINDOWS_PHONE
using System.Text.RegularExpressions;
#endif
using SimplyMobile.Text;
using System.Text;

namespace SimplyMobile.Web
{
    public partial class WebHybrid : IWebHybrid
    {
#if !WINDOWS_PHONE
        private const string Format = "file://(local|LOCAL)/Action=(?<Action>[\\w]+)/";
        private static readonly Regex Expression = new Regex(Format);
#endif
        private Dictionary<string, Action<string>> registeredActions;

        partial void Inject(string script);
        partial void LoadFile(string fileName);

        public IJsonSerializer Serializer { get; set; }

        public void InjectJavaScript(string script)
        {
            this.Inject(script);
        }

        public void RegisterCallback(string name, Action<string> action)
        {
            this.registeredActions.Add(name, action);
        }

        public bool RemoveCallback(string name)
        {
            return this.registeredActions.Remove(name);
        }

        public void CallJsFunction(string funcName, params object[] parameters)
        {
            var builder = new StringBuilder();

            builder.Append(funcName);
            builder.Append("(");

            for (var n = 0; n < parameters.Length; n++)
            {
                builder.Append(this.Serializer.Serialize(parameters[n]));
                if (n < parameters.Length - 1) 
                {
                    builder.Append(", ");
                }
            }

            builder.Append(");");

            this.Inject(builder.ToString());
        }

        public void LoadFromFile(string fileName)
        {
        }


        private void InjectNativeFunctionScript()
        {
            var builder = new StringBuilder();
            builder.Append("function Native(action, data){ ");
#if !WINDOWS_PHONE
            builder.Append("window.location = \"//LOCAL/Action=\" + ");
#else
            builder.Append("window.external.notify(");
#endif
            builder.Append("action + \"/\"");
            builder.Append(" + ((typeof data == \"object\") ? JSON.stringify(data) : data)");
#if WINDOWS_PHONE
            builder.Append(")");
#endif
            builder.Append(" ;}");

            this.Inject(builder.ToString());
        }

#if !WINDOWS_PHONE
        private bool CheckRequest(string request)
        {
            var m = Expression.Match(request);

            if (m.Success) 
            {
                Action<string> action;
                var name = m.Groups["Action"].Value;

                if (this.registeredActions.TryGetValue(name, out action))
                {
                    var data = Uri.UnescapeDataString(request.Remove(m.Index, m.Length));
                    action.Invoke(data);
                    return true;
                }
            }

            return false;   
        }
#endif
    }
}

