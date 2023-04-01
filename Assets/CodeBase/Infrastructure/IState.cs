namespace Assets.CodeBase.Infrastructure
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IGameExitableState : IExitableState { }
    public interface IGameState : IGameExitableState, IState { }
    public interface IPayloadedGameState<TPayload> : IGameExitableState, IPayloadedState<TPayload> { }

    public interface IUnitExitableState : IExitableState, IUpdatable { }
    public interface IUnitState : IUnitExitableState, IState { }
    public interface IPayloadedUnitState<TPayload> : IUnitExitableState, IPayloadedState<TPayload> { }
}