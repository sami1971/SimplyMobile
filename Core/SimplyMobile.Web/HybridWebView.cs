using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace SimplyMobile.Web
{
    public partial class HybridWebView
    {
        private const string Format = "file://local/Action=(?<Action>[\\w]+)/";
        private static readonly Regex Expression = new Regex(Format);
        private Dictionary<string, Action<string>> registeredActions;

        partial void Inject(string script);

        public void InjectJavaScript(string script)
        {
            this.Inject(script);
        }

        public void RegisterCallback(string name, Action<string> action)
        {
            this.registeredActions.Add(name, action);
        }

        private bool CheckRequest(string request)
        {
            var m = Expression.Match(request);

            if (m.Success) 
            {
                Action<string> action;
                var name = m.Groups ["Action"].Value;

                if (this.registeredActions.TryGetValue(name, out action))
                {
                    var data = Uri.UnescapeDataString(request.Remove(m.Index, m.Length));
                    action.Invoke (data);
                    return false;
                }
            }

            return true;    
        }

        private void InjectNativeFunctionScript()
        {
            var builder = new StringBuilder ();
            builder.Append("function Native(action, data){ ");
            builder.Append("window.location = \"//LOCAL/Action=\" + action + \"/\" + JSON.stringify(data);");
            builder.Append(" }");
            Inject(builder.ToString());
        }
    }
}

