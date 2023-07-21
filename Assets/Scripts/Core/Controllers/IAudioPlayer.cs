using UnityEngine;

namespace Core.Controllers
{
    public interface IAudioPlayer
    {
        void Stop();
        void PlayOneShot(AudioClip clip);
    }
}