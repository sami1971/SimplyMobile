using System;
using System.IO;

namespace SimplyMobile.Text
{
    public interface IStreamLocator
    {
        StreamWriter StreamWriterFor(string path);
    }
}

