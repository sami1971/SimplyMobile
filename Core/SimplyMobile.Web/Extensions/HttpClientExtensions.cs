using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Web
{
    public static class HttpClientExtensions
    {
        public static async Task DownloadFile(this HttpClient httpClient, string url, string destination)
        {
            var stream = await httpClient.GetStreamAsync(url);

            using (var writer = new StreamWriter(destination))
            {
                await stream.CopyToAsync(writer.BaseStream);
            }
        }
    }
}
