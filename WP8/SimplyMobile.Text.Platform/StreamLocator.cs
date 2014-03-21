using System;
using System.IO;

namespace SimplyMobile.Text
{
    public class StreamLocator : IStreamLocator
    {
        public string CurrentPath
        {
            get { return Environment.CurrentDirectory; }
        }

        public string AppFolder
        {
#if WINDOWS_PHONE
            get { return Windows.Storage.ApplicationData.Current.LocalFolder.Path; }
#else
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
#endif
        }

        public StreamWriter StreamWriterFor(string path)
        {
            return new StreamWriter(path);
        }

        public BinaryWriter BinaryWriterFor(string path)
        {
            return new BinaryWriter(File.OpenWrite(path));
        }
    }
}
