using System;
using System.Threading.Tasks;

namespace SimplyMobile.Core
{
    public interface IMediaPlayer
    {
        Task<bool> Play(Uri uri);
        Task<bool> Play(string filename);
        void Stop();
    }
}

