using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using Stats;
using UnityEngine;

namespace Utilities
{
    public static class ConditionUtility
    {
        private static readonly IDictionary<RelationSide, HashSet<Tag>> UnitReleationSides = new Dictionary<RelationSide, HashSet<Tag>>
        {
            {RelationSide.PlayerSide, new HashSet<Tag> { Tag.Player, Tag.Ally }},
            {RelationSide.EnemySide, new HashSet<Tag> { Tag.Enemy }}
        };

        /// <summary>
        /// Checks whether the given tag equals one of the Tags.
        /// </summary>
        /// <param name="Tags">Tags to check against</param>
        /// <param name="tag">TargetUnitRelation to check against</param>
        /// <returns>True if TargetUnitRelation is part of Tags or Tags is empty, false otherwise</returns>
        public static bool CheckIfTagsMatch(string[] Tags, string tag)
        {
            if (Tags.Length <= 0) return true;
            var rightTag = Tags.Any(t => t == tag);

            return rightTag;
        }

        public static bool CheckProbability(int chance)
        {
            return ValueUtility.GetRandom(1, 101) <= chance;
        }

        public static bool CheckResistance(int mentalPower, int mentalResistance)
        {
            if (mentalPower == 0)
            {
                return false;
            }

            if (mentalResistance == 0)
            {
                return true;
            }

            var d = (mentalPower * 1.0)/mentalResistance;
            if (d >= 2)
            {
                return true;
            }

            return ValueUtility.GetRandom(0, 2.0f) < d;
        }

        public static bool CheckUnitRelationship(ISkillCaster caster, IStats target, TargetUnitRelation targetUnitRelation)
        {
            return target != null && CheckUnitRelationship(caster.Characteristics.Tag, target.Characteristics.Tag, targetUnitRelation);
        }

        public static bool CheckUnitRelationship(
            Tag unitTag1, Tag unitTag2, TargetUnitRelation targetUnitRelation)
        {
            if (targetUnitRelation == TargetUnitRelation.None)
            {
                return true;
            }
            
            if (targetUnitRelation == TargetUnitRelation.PlayerOnly)
            {
                return unitTag2 == Tag.Player;
            }

            if (targetUnitRelation == TargetUnitRelation.DeadOnly)
            {
                return unitTag2 == Tag.Dead;
            }

            var unit1Side = GetUnitReleationSideByTag(unitTag1);
            var unit2Side = GetUnitReleationSideByTag(unitTag2);
            if (unit1Side == RelationSide.Default || unit2Side == RelationSide.Default)
            {
                return false;
            }

            return targetUnitRelation == TargetUnitRelation.Enemy 
                ? unit1Side != unit2Side : unit1Side == unit2Side;
        }

        public static bool CheckUnitRelationship(
            Tag unitTag1,
            string unitTag2,
            TargetUnitRelation targetUnitRelation)
        {
            return CheckUnitRelationship(unitTag1, GetTag(unitTag2), targetUnitRelation);
        }

        public static bool CheckRelationSideByTag(
            Tag tag, RelationSide expectedRelationSide)
        {
            return GetUnitReleationSideByTag(tag) == expectedRelationSide;
        }

        public static bool IsVectorOnCollider(Collider2D collider, Vector2 vector)
        {
            return collider.bounds.min.x < vector.x
                   && collider.bounds.min.y < vector.y
                   && collider.bounds.max.x > vector.x
                   && collider.bounds.max.y > vector.y;
        }

        public static bool IsValueDefault<T>(T value)
            where T : struct 
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }

        private static RelationSide GetUnitReleationSideByTag(Tag tag)
        {
            foreach (var relationSide in UnitReleationSides.Keys.Where(relationSide =>
                UnitReleationSides[relationSide].Contains(tag)))
            {
                return relationSide;
            }

            return RelationSide.Default;
        }

        private static Tag GetTag(string tag)
        {
            switch (tag)
            {
                case "Player": return Tag.Player;
                case "Enemy": return Tag.Enemy;
                case "Ally": return Tag.Ally;
                case "Dead": return Tag.Dead;
                case "Default": return Tag.Default;
            }
            
            throw new NotSupportedException();
        }
    }
}
