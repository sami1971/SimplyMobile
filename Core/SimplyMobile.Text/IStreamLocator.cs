using System;
using System.IO;
using System.Collections.Generic;

namespace SimplyMobile.Text
{
    public interface IStreamLocator
    {
        string CurrentPath { get; }
        string AppFolder { get; }

        StreamWriter StreamWriterFor(string path);
        BinaryWriter BinaryWriterFor(string path);

        IEnumerable<string> GetFolderNames(string folder, string searchPattern);
        IEnumerable<string> GetFileNames(string folder, string searchPattern);
    }

    public static class StreamLocatorExtensions
    {
        /// <summary>
        /// Gets a stream writer for combined path
        /// </summary>
        /// <returns>StreamWriter for the file.</returns>
        /// <param name="args">Paths and filename.</param>
        public static StreamWriter StreamWriterFor(this IStreamLocator locator, string[] args)
        {
            return locator.StreamWriterFor (Path.Combine (args));
        }

        /// <summary>
        /// Gets a stream writer for file in current path
        /// </summary>
        /// <returns>StreamWriter for the file.</returns>
        /// <param name="fileName">Filename.</param>
        public static StreamWriter StreamWriterCurrent(this IStreamLocator locator, string fileName)
        {
            return locator.StreamWriterFor (Path.Combine (locator.CurrentPath, fileName));
        }

        /// <summary>
        /// Gets a binary writer for combined path
        /// </summary>
        /// <returns>StreamWriter for the file.</returns>
        /// <param name="args">Paths and filename.</param>
        public static BinaryWriter BinaryWriterFor(this IStreamLocator locator, string[] args)
        {
            return locator.BinaryWriterFor (Path.Combine (args));
        }

        /// <summary>
        /// Gets a binary writer for combined path
        /// </summary>
        /// <returns>StreamWriter for the file.</returns>
        /// <param name="args">Paths and filename.</param>
        public static BinaryWriter BinaryWriterCurrent(this IStreamLocator locator, string fileName)
        {
            return locator.BinaryWriterFor (Path.Combine (locator.CurrentPath, fileName));
        }
    }
}

