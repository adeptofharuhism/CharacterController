using Assets.CodeBase.Infrastructure;

namespace Assets.CodeBase.Character.States
{
    public abstract class UnitStateMachine : StateMachine<IUnitExitableState>, IUpdatable
    {
        public void HandleInput() => _activeState?.HandleInput();

        public void Update() => _activeState?.Update();

        public void PhysicsUpdate() => _activeState?.PhysicsUpdate();
    }
}
