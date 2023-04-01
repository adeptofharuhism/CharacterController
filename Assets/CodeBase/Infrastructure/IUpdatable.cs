namespace Assets.CodeBase.Infrastructure
{
    public interface IUpdatable
    {
        void HandleInput();
        void Update();
        void PhysicsUpdate();
    }
}
