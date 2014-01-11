using System;
using System.Collections.Generic;
using System.Linq;
#if !WINDOWS_PHONE
using System.Text.RegularExpressions;
using System.Text;
#endif
using SimplyMobile.Text;

namespace SimplyMobile.Web
{
	public partial class WebHybrid
	{
#if !WINDOWS_PHONE
		private const string Format = "file://(local|LOCAL)/Action=(?<Action>[\\w]+)/";
		private static readonly Regex Expression = new Regex(Format);
#endif
		private Dictionary<string, Action<string>> registeredActions;

		partial void Inject(string script);

		public IJsonSerializer Serializer { get; set; }

		public void InjectJavaScript(string script)
		{
			this.Inject(script);
		}

		public void RegisterCallback(string name, Action<string> action)
		{
			this.registeredActions.Add(name, action);
		}

		public void CallJsFunction(string funcName, params object[] parameters)
		{
#if WINDOWS_PHONE
		    try
		    {
                var args = parameters.Select(a => this.Serializer.Serialize(a)).ToArray();
                var resp = this.webView.InvokeScript(funcName, args);
		    }
		    catch (Exception exception)
		    {
		        System.Diagnostics.Debug.WriteLine(exception.Message);
		    }
#else
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
#endif
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

		private void InjectNativeFunctionScript()
		{
			var builder = new StringBuilder();
			builder.Append("function Native(action, data){ ");
			builder.Append("window.location = \"//LOCAL/Action=\" + action + \"/\" + JSON.stringify(data);");
			builder.Append(" }");
			this.Inject(builder.ToString());
		}
#endif
    }
}

