using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Properties;

namespace Assets.CodeBase.Character.States
{
    public abstract class UnitStateMachine : StateMachine<IUnitExitableState>, IUpdatable, IAnimationEventUser
    {
        public void HandleInput() => _activeState?.HandleInput();

        public void Update() => _activeState?.Update();

        public void PhysicsUpdate() => _activeState?.PhysicsUpdate();

        public void OnAnimationEnterEvent() => _activeState?.OnAnimationEnterEvent();

        public void OnAnimationExitEvent() => _activeState?.OnAnimationExitEvent();

        public void OnAnimationTransitEvent() => _activeState?.OnAnimationTransitEvent();
    }
}
