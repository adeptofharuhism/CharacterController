namespace Assets.CodeBase.Character.States
{
    public interface IUnitState
    {
        void Enter();
        void Exit();

        void HandleInput();
        void Update();
        void PhysicsUpdate();
    }
}
