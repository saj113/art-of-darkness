using Skills.Particles;
using Stats;
using UnityEngine;

namespace Skills.Behaviors
{
    public class AnimationSkillParticles : SkillParticles
    {
        [SerializeField] private float _shiftParticlesX;
        [SerializeField] private float _shiftParticlesY;

        public void Initialize(ISkillCaster caster)
        {
            var casterPosition = caster.GameObjectController.CenterPosition;
            var targetXCoordinate = casterPosition.x;
            var targetYCoordinate = Mathf.Abs(_shiftParticlesY) > 0.01f
                ? casterPosition.y + _shiftParticlesY
                : casterPosition.y;

            if (Mathf.Abs(_shiftParticlesX) > 0.01f)
            {
                
                if (caster.Characteristics.IsFacingRight)
                {
                    targetXCoordinate += _shiftParticlesX;
                }
                else
                {
                    targetXCoordinate -= _shiftParticlesX;
                    transform.rotation = new Quaternion(
                        transform.rotation.x, transform.rotation.y * -1, transform.rotation.z, transform.rotation.w);
                }
            }

            SetPosition(new Vector2(targetXCoordinate, targetYCoordinate));
        }
    }
}
