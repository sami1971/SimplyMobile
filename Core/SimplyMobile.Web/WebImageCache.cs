using System;
using System.Threading.Tasks;

namespace SimplyMobile.Web
{
    public class WebImageCache
    {
        private string localPath;

        public WebImageCache (string localPath)
        {
            this.localPath = localPath;
        }

        public void Download(string uri, Action<string> onSuccess)
        {

        }
    }
}

