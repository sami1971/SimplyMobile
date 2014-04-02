using System;
using SimplyMobile.Text;
using System.IO;

namespace StreamLocatorDependency
{
    public class StreamWriterFunction
    {
        private readonly IStreamLocator locator;

        public StreamWriterFunction (IStreamLocator locator)
        {
            this.locator = locator;
        }

        public void WriteToPath(string path, string text)
        {
            using (var writer = this.locator.StreamWriterFor (path))
            {
                writer.WriteLine (text);
            }
        }
    }
}

