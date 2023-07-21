using System;
using Skills.Cooldown;
using UnitControllers.TouchControllers;
using UnityEngine;

namespace Skills.Parameters
{
    public interface IGeneralParameters : IDisposable
    {
        Sprite Icon { get; }
        ShapeType ShapeType { get; }
        float Range { get; }
        int Charges { get; }
        ISkillCooldown[] SkillCooldownCollection { get; }
    }
}
