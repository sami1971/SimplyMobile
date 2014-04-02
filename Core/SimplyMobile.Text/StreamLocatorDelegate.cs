using System;
using System.IO;

namespace SimplyMobile.Text
{
    public class StreamLocatorDelegate : IStreamLocator
    {
        public delegate StreamWriter StreamLocator(string path);

        private readonly StreamLocator locator;

        public StreamLocatorDelegate (StreamLocator locator)
        {
            this.locator = locator;
        }

        #region IStreamLocator implementation

        public StreamWriter StreamWriterFor(string path)
        {
            return locator (path);
        }

        #endregion

        public BinaryWriter BinaryWriterFor(string path)
        {
            throw new NotImplementedException();
        }

        public string CurrentPath
        {
            get { throw new NotImplementedException(); }
        }
    }
}

