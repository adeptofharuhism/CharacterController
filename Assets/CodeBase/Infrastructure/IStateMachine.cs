using Assets.CodeBase.Infrastructure.Services;

namespace Assets.CodeBase.Infrastructure
{
    public interface IStateMachine : IService
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState>() where TState : class, IState;
    }
}