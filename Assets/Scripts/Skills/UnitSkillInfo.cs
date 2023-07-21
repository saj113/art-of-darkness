using System;
using Spine.Unity;
using UnityEngine;

namespace Skills
{
    public interface IUnitSkillInfo
    {
        UnitSkillName Name { get; }
        string[] AnimationNames { get; }
        AudioClip Sound { get; }
        Vector2 DeviationFromCenter { get; }
    }

    [Serializable]
    public class UnitSkillInfo : IUnitSkillInfo
    {
        [SerializeField] private UnitSkillName _name;

        [SpineAnimation()] [SerializeField] private string[] _animationNames;

        [SerializeField] private AudioClip _sound;

        [SerializeField] private Vector2 _deviationFromCenter;

        public UnitSkillName Name
        {
            get { return _name; }
        }

        public string[] AnimationNames
        {
            get { return _animationNames; }
        }

        public AudioClip Sound
        {
            get { return _sound; }
        }

        public Vector2 DeviationFromCenter
        {
            get { return _deviationFromCenter; }
        }
    }
}
