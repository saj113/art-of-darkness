using System;
using System.Linq;
using Core;
using Skills.Cooldown;
using UnitControllers.TouchControllers;
using UnityEngine;
using Utilities;

namespace Skills.Parameters
{
    public class GeneralParameters : IGeneralParameters
    {
        public GeneralParameters(
            ISkillCooldown skillCooldown,
            Sprite icon = null, 
            ShapeType shapeType = ShapeType.None, 
            float range = 30, 
            int charges = 1)
        : this(new ISkillCooldown[1] { skillCooldown }, icon, shapeType, range, charges)
        {
        }
        public GeneralParameters(
            ISkillCooldown[] skillCooldownCollection,
            Sprite icon = null, 
            ShapeType shapeType = ShapeType.None, 
            float range = 30, 
            int charges = 1)
        {
            Icon = icon;
            ShapeType = shapeType;
            Range = range;
            Charges = charges;
            SkillCooldownCollection = skillCooldownCollection;
            
            Contract.Ensure(range > 0, "RangeInMeters has invalid value");
            Contract.Ensure(Charges > 0, "Charges has invalid value");
            Contract.Ensure(SkillCooldownCollection != null, "SkillCooldownCollection is null");
            Contract.Ensure(SkillCooldownCollection.Any(), "SkillCooldownCollection is empty");
        }

        public Sprite Icon { get; private set; }
        public ShapeType ShapeType { get; private set; }
        public float Range { get; private set; }
        public int Charges { get; private set; }
        public ISkillCooldown[] SkillCooldownCollection { get; private set; }

        public SkillCooldownType SkillCooldownType { get; private set; }
        public int RequiredMana { get; private set; }
        public int RequiredHealthInPercent { get; private set; }

        public void Dispose()
        {
            SkillCooldownCollection.DisposeAll();
        }
    }
}