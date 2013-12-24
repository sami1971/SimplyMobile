using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace SimplyMobile.Web.CanvasJs
{
    public partial class CanvasView
    {
        public CanvasView(WebBrowser browser)
            : base(browser)
        {

        }

        public void Load()
        {
            var fileName = "chart.html"; // remember case-sensitive
            var localHtmlUrl = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            if (File.Exists(localHtmlUrl))
            {
                this.webView.Navigate(new Uri(localHtmlUrl));
            }
        }
    }
}
