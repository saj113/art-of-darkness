using Skills.Parameters;
using UnitControllers.TouchControllers;
using UnityEngine;

namespace Skills.Weapons
{
    public interface ICharacterWeapon : IWeapon
    {
        IExtendedSkillParameters[] ExtendedSkillParameters { get; }
        ISkillParameters[] ShapeSkillParameters { get; }
        IExtendedSkillParameters[] SealSkillParameters { get; }
        SkillUseFailedReason UseShapeSkill(ShapeType shapeType, float position);
        SkillUseFailedReason UseSkill(ISkillParameters skillParameters);
        void StopSkill();
    }
}
