using System;
using System.Collections.Generic;
using System.Linq;
using Core.UnityFramework;
using Skills;
using Skills.Modificators;
using Stats;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Core
{
    /// <summary>
    /// Stores extensions for the different types.
    /// </summary>
    public static class Extensions
    {
        public static T GetRandomElement<T>(this IEnumerable<T> elements)
        {
            if (elements == null || !elements.Any())
            {
                return default(T);
            }

            var randomIndex = ValueUtility.GetRandom(0, elements.Count());
            if (randomIndex == 0)
            {
                return elements.First();
            }

            return elements.Skip(randomIndex - 1).First();
        }

        public static bool HasFlag(this SkillCooldownType flags, SkillCooldownType flagToTest)
        {
            if (flagToTest == 0)
            {
                throw new ArgumentOutOfRangeException("flagToTest", "Value must not be 0");
            }

            return (flags & flagToTest) == flagToTest;
        }

        public static void ApplyAll(this IModificator[] modificators, IStats target)
        {
            foreach (var modificator in modificators)
            {
                modificator.Apply(target);
            }
        }

        public static void Register(this EventTrigger trigger, EventTriggerType triggerType, Action callback)
        {
            var pointerDownEntry = new EventTrigger.Entry { eventID = triggerType };
            pointerDownEntry.callback.AddListener(p => callback());
            trigger.triggers.Add(pointerDownEntry);
        }

        public static float Distance(this Vector2 from, Vector2 to)
        {
            return from.x.Distance(to.x);
        }

        public static float Distance(this float fromX, float toX)
        {
            float num1 = fromX - toX;
            return (float) Math.Sqrt((double) num1 * (double) num1);
        }

        public static void DisposeAll(this IEnumerable<IDisposable> disposables)
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }

        public static T ThrowIfNull<T>(this T obj, string objName)
        {
            if (obj == null) throw new ArgumentNullException(objName);
            return obj;
        }
    }
}
