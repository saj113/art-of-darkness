using System;

namespace Core.UnityFramework
{
    public interface IUnityUpdateEvents
    {
        event Action<float> UpdateFired;
        event Action<float> FixedUpdateFired;
        event Action EverySecond;
    }
}
