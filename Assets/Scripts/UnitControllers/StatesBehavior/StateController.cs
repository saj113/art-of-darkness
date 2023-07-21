using System;
using System.Collections.Generic;
using Core;
using Core.Animation;
using Core.UnityFramework;
using Skills.Weapons;
using UnitControllers.States;

namespace UnitControllers.StatesBehavior
{
    internal abstract class StateController : IStateController
    {
        private IState _registeredState;
        private readonly IDictionary<StateType, IState> _cachedStates;
        private readonly IUnityUpdateEvents _unityUpdateEvents;

        protected StateController(
            IStateAnimationController stateAnimationController,
            IUnityUpdateEvents updateEvents, 
            IWeapon weapon,
            ILogger logger)
        {
            AnimationController = stateAnimationController;
            Weapon = weapon;
            Logger = logger;
            _unityUpdateEvents = updateEvents;
            _unityUpdateEvents.FixedUpdateFired += UpdateEventsOnFixedUpdateFired;
            _cachedStates = new Dictionary<StateType, IState>();
        }

        public StateType CurrentStateType
        {
            get { return _registeredState.Type; }
        }

        protected IStateAnimationController AnimationController { get; private set; }
        protected IWeapon Weapon { get; private set; }
        protected ILogger Logger {get; private set;}
        public abstract void SetIdleState();
        public abstract void SetMoveState();
        public abstract void SetDeadState();

        public void SetStunState()
        {
            SetState(StateType.StunState);
        }

        public void SetFearState()
        {
            SetState(StateType.FearState);
        }

        public void SetFlyState()
        {
            SetState(StateType.FlyState);
        }

        public void SetAttackState()
        {
            SetState(StateType.AttackState);
        }

        private void UpdateEventsOnFixedUpdateFired(float f)
        {
            if (_registeredState != null)
            {
                _registeredState.Update();
            }
        }

        protected virtual IState GetStateByType(StateType type)
        {
            switch (type)
            {
                case StateType.AttackState:
                    return new AttackState(this, Weapon);
                case StateType.FearState:
                    return new FearState(AnimationController);
                case StateType.StunState:
                    return new StunState(AnimationController);
                case StateType.FlyState:
                    return new FlyState(AnimationController);
            }

            throw new NotSupportedException(
                string.Format("The type '{0}' is not supported!", type));
        }

        protected void SetState(StateType type)
        {
            Logger.LogInfo("SetState: " + type.ToString());
            if (_registeredState != null)
            {
                if (CurrentStateType == type)
                {
                    return;
                }

                _registeredState.ResetState();
            }

            IState cachedState;
            if (!_cachedStates.TryGetValue(type, out cachedState))
            {
                cachedState = GetStateByType(type);
                _cachedStates.Add(type, cachedState);
            }

            _registeredState = cachedState;
            _registeredState.EnableState();
        }

        public void Dispose() 
        { 
            _unityUpdateEvents.FixedUpdateFired -= UpdateEventsOnFixedUpdateFired;
            _registeredState.ResetState();
        }
    }
}
