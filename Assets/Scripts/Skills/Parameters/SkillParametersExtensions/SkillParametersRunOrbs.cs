/* using System.Collections.Generic;
using Assets.Scripts.Core.UnityFramework.Interfaces;
using Assets.Scripts.Skills.Parameters.BehaviorParameters;
using Assets.Scripts.Skills.SerializedData;
using Assets.Scripts.StatsComponents;
using Assets.Scripts.StatsComponents.UnitControllers;
using UnityEngine;

namespace Assets.Scripts.Skills.Parameters.SkillParametersExtensions
{
    public class SkillParametersRunOrbs : SkillParameters
    {
        private float _timeElapsed = 0;

        private readonly Queue<ParticleSystem> _orbCollection = new Queue<ParticleSystem>();
        private IList<ParticleSystem> _activeOrbs;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IGameObjectInstantiater _unityObjectContoller;
        private readonly ParticleSystem _orbParticleSystemPrefab;
        private readonly ICharacteristics _characteristics;

        private bool _isInitialized;

        public SkillParametersRunOrbs(
            ParticleSystem orbParticlesPrefab, 
            ICharacteristics characteristics,
            IUnitGameObjectController unitGameObjectController,
            IGameObjectInstantiater unityObjectContoller,
            IGeneralParameters general,
            IAnimationParameters animation,
            IBehaviorsParameters behaviorParameters)
            : base(general, animation, behaviorParameters)
        {
            _characteristics = characteristics;
            _orbParticleSystemPrefab = orbParticlesPrefab;
            _unityObjectContoller = unityObjectContoller;
            _unitGameObjectController = unitGameObjectController;
            BehaviorParameters.MovementSkillParticlesParameters.ParticlesInstancesCount = General.Charges;
        }

        public override void NotifySkillExecuted()
        {
            BehaviorParameters.MovementSkillParticlesParameters.ParticlesInstancesCount = 0;
            _timeElapsed = 0;
            CreateOrb();
        }

        public override void NotifySkillExecuting()
        {
            foreach (var orb in _activeOrbs)
            {
                orb.gameObject.SetActive(false);
                _orbCollection.Enqueue(orb);
            }

            _timeElapsed = 0;
            _activeOrbs.Clear();
        }

        public override void Update(float deltaTime)
        {
            if (!_isInitialized)
            {
                CurrentCharges = 1;
                _activeOrbs = new List<ParticleSystem>(
                    InstantiateOrbParticles(_orbParticleSystemPrefab));
                _isInitialized = true;
            }

            TryActivateOrb(deltaTime);

            //base.Update(deltaTime);

            UpdateOrbsMovement();
        }

        private void TryActivateOrb(float deltaTime)
        {
            if (_activeOrbs.Count < General.Charges)
            {
                _timeElapsed += deltaTime;
                if (_timeElapsed > General.CoolDown)
                {
                    _timeElapsed = 0;
                    CreateOrb();
                }
            }
        }

        private void CreateOrb()
        {
            var orb = _orbCollection.Dequeue();
            orb.gameObject.SetActive(true);
            orb.transform.position = _unitGameObjectController.CenterPosition;
            _activeOrbs.Add(orb);
            BehaviorParameters.MovementSkillParticlesParameters.ParticlesInstancesCount++;
        }

        private void UpdateOrbsMovement()
        {
            var orbsCount = _activeOrbs.Count;

            for (var i = 0; i < orbsCount; i++)
            {
                var angle = (float)(i * Mathf.PI * 0.5 / orbsCount + Mathf.PI / 3);

                var orbX = _unitGameObjectController.CenterPosition.x + Mathf.Cos(angle);
                var orbY = _unitGameObjectController.CenterPosition.y + Mathf.Sin(angle);

                _activeOrbs[i].transform.position = Vector3.MoveTowards(
                    _activeOrbs[i].transform.position,
                    new Vector2(orbX, orbY * 2),
                    _characteristics.MovementSpeed * Time.deltaTime);
            }
        }


        private IEnumerable<ParticleSystem> InstantiateOrbParticles(ParticleSystem prefab)
        {
            for (var i = 0; i < General.Charges; i++)
            {
                var orb = _unityObjectContoller.Instantiate(prefab);
                orb.transform.position = _unitGameObjectController.CenterPosition;
                yield return orb;
            }
        }
    }
}
*/