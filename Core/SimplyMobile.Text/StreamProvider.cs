using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Text
{
    public static class StreamProvider
    {
        private static IStreamLocator locator;

        private static IStreamLocator StreamLocator
        {
            get
            {
                if (locator == null)
                {
                    throw new NullReferenceException("You will need to set StreamProvider.Locator prior to using this class.");
                }

                return locator;
            }
        }

        public static IStreamLocator Locator 
        { 
            set { locator = value; }
            get { return locator; }
        }

        public static string CurrentPath
        {
            get { return StreamLocator.CurrentPath; }
        }

        public static string AppFolder
        {
            get { return StreamLocator.AppFolder; }
        }

        public static System.IO.StreamWriter StreamWriterFor(string path)
        {
            return StreamLocator.StreamWriterFor(path);
        }

        public static System.IO.BinaryWriter BinaryWriterFor(string path)
        {
            return StreamLocator.BinaryWriterFor(path);
        }

        public static IEnumerable<string> GetFolderNames(string folder, string searchPattern)
        {
            return StreamLocator.GetFolderNames (folder, searchPattern);
        }

        public static IEnumerable<string> GetFileNames(string folder, string searchPattern)
        {
            return StreamLocator.GetFileNames (folder, searchPattern);
        }
    }
}
