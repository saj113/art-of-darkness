using System;
using UnityEngine.EventSystems;

namespace GUIScripts.Triggers
{
    public interface IClickableSkillTrigger : ISkillTrigger
    {
        event Action PointerDown;
        event Action PointerUp;

        EventTrigger EventTrigger { get; }
    }
}