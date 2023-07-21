using Core;
using Core.Trigger;
using Skills.Behaviors.RunPsBehaviors;
using Skills.Behaviors.RunPsBehaviors.CollisionBehaviors;
using Stats;
using UnityEngine;

namespace Skills.Particles
{
    [RequireComponent(typeof(OnTrigger))]
    public class MovementSkillParticles : SkillParticles, IMovementSkillParticles
    {
        [SerializeField] private TargetUnitRelation _targetUnitRelation = TargetUnitRelation.Enemy;
        [SerializeField]
        private float _moveSpeed = 10;
        [SerializeField]
        private float _moveMaxDistance = 60;
        [SerializeField]
        private float _randomMoveRadius;
        [SerializeField]
        private float _randomMoveSpeed;
        [SerializeField]
        private float _randomRange;
        [SerializeField]
        private float _moveDistanceFactor = 20f;
        [SerializeField]
        private bool _isDeviation;

        private float _randomRadiusX, _randomRadiusY;
        private int _randomDirection1, _randomDirection2, _randomDirection3;
        private float _startTime;
        private float _deltaSpeed;

        private Vector2 _oldSmootRandomPos = Vector2.zero;
        private Vector2 _startPosition = Vector2.zero;

        private ParticlesTarget _particlesTarget;
        private ICollisionBehavior _collisionBehavior;
        private OnTrigger _trigger;
        private CollisionEvents _collisionEvents;

        public void Initialize(Tag ownerTag, ParticlesTarget particlesTarget, ICollisionBehavior collisionBehavior, Vector2 startPosition)
        {
            _particlesTarget = particlesTarget;
            transform.position = startPosition;
            _collisionBehavior = collisionBehavior;
            _collisionEvents = new CollisionEvents(ownerTag, _targetUnitRelation, GetComponent<Collider2D>());
            _collisionEvents.UnitCollisionEntered += OnTriggerEntered;
        }

        public override void StopEmission()
        {
            base.StopEmission();
            _collisionEvents.UnitCollisionEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(IStats target, Vector2 pos)
        {
            if (_collisionBehavior.ForwardUnitCollision(target, pos))
            {
                StopEmission();
            }
        }

        private void OnProjectileBarrierTriggered(OnTrigger sender, Tag tag, Vector2 pos)
        {
            if (_collisionBehavior.ForwardBarrierCollision(tag, pos))
            {
                StopEmission();
            }
        }

        protected override void VirtualStart()
        {
            InitializeDefault();
        }

        protected override void VirtualUpdate()
        {
            ProceedMovement();
            _collisionEvents.Update();
            CheckVisualizationDistanceToCamera();
        }

        private void ProceedMovement()
        {
            //TODO: implement multiply particles movement
            var amount = 0;

            var smootRandomPos = GetSmootRandomhPos(_particlesTarget.GetTargetPosition(), _startPosition, amount++);
            var delta = smootRandomPos - _oldSmootRandomPos;
            var moveDistance = Vector2.MoveTowards(
                transform.position, _particlesTarget.GetTargetPosition(), _moveSpeed * Time.deltaTime);
            var moveDistanceRandom = moveDistance + delta;
            transform.position = moveDistanceRandom;
            _oldSmootRandomPos = smootRandomPos;
        }

        private void CheckVisualizationDistanceToCamera()
        {
            var distanceToCamera = Vector2.Distance(
                                transform.position, UnityEngine.Camera.main.transform.position);
            if (distanceToCamera > _moveMaxDistance)
            {
                StopEmission();
            }
        }

        
        private void InitializeDefault()
        {
            _startTime = 0;
            _randomDirection1 = 0;
            _randomDirection2 = 0;
            _randomDirection3 = 0;
            _startPosition = transform.position;
            InitRandomVariables();
        }

        #region Setup random direction behavior properties

        private void InitRandomVariables()
        {
            _startTime = Time.time;
            _randomRadiusX = Random.Range(_randomMoveRadius, _randomMoveRadius * 2) / 100;
            _randomRadiusY = Random.Range(_randomMoveRadius, _randomMoveRadius * 2) / 100;
            _deltaSpeed = (_randomMoveSpeed / 2) * Random.Range(1, 1000 * _randomRange + 1) / 1000 - 1;
            _randomDirection1 = Random.Range(0, 2) > 0 ? 1 : -1;
            _randomDirection2 = Random.Range(0, 2) > 0 ? 1 : -1;
            _randomDirection3 = Random.Range(0, 2) > 0 ? 1 : -1;
        }

        private Vector2 GetSmootRandomhPos(Vector3 target, Vector2 startPosition, int amount)
        {
            var time = (Time.time - _startTime);

            var timeDegree = time * _randomMoveSpeed + amount;
            var delta = time * _deltaSpeed;

            var coord1 = _randomDirection2 * Mathf.Sin(timeDegree) * _randomRadiusX;
            var coord2 = _randomDirection3 * Mathf.Sin(timeDegree + (_randomDirection1 * Mathf.PI / 2) * time + Mathf.Sin(delta)) *
                     _randomRadiusY;

            if (_isDeviation)
            {
                var deviation = Vector2.Distance(
                    startPosition, target) / _moveDistanceFactor;

                coord1 *= deviation;
                coord2 *= deviation;
            }

            return new Vector2(coord1, coord2);
        }
        #endregion
    }
}
