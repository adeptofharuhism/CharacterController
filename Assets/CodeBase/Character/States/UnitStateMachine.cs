using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Character.States
{
    public abstract class UnitStateMachine
    {
        protected Dictionary<Type, IUnitState> _states;
        protected IUnitState _activeState;

        public void HandleInput() => _activeState?.HandleInput();

        public void Update() => _activeState?.Update();

        public void PhysicsUpdate() => _activeState?.PhysicsUpdate();

        public void Enter<TState>() where TState : class, IUnitState {
            TState state = ChangeState<TState>();

            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IUnitState {
            _activeState?.Exit();

            TState newState = GetState<TState>();
            _activeState = newState;

            return newState;
        }

        private TState GetState<TState>() where TState : class, IUnitState =>
            _states[typeof(TState)] as TState;
    }
}
