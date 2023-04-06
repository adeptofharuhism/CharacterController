using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Properties;
using UnityEngine;

namespace Assets.CodeBase.Character.States
{
    public abstract class UnitStateMachine : 
        StateMachine<IUnitExitableState>, IUpdatable, IAnimationEventUser, ITriggerable
    {
        public void HandleInput() => _activeState?.HandleInput();

        public void Update() => _activeState?.Update();

        public void PhysicsUpdate() => _activeState?.PhysicsUpdate();

        public void OnAnimationEnterEvent() => _activeState?.OnAnimationEnterEvent();

        public void OnAnimationExitEvent() => _activeState?.OnAnimationExitEvent();

        public void OnAnimationTransitEvent() => _activeState?.OnAnimationTransitEvent();

        public void OnTriggerEnter(Collider collider) => _activeState?.OnTriggerEnter(collider);

        public void OnTriggerExit(Collider collider) => _activeState?.OnTriggerExit(collider);
    }
}
