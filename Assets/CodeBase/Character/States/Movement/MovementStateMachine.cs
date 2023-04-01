using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.States;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementStateMachine : UnitStateMachine
    {
        private readonly Player.Player _player;

        public Player.Player Player => _player;

        public MovementStateMachine(Player.Player player) {
            _player = player;

            _states = new Dictionary<Type, IUnitExitableState>() {
                [typeof(IdlingState)] = new IdlingState(this),
                [typeof(WalkingState)] = new WalkingState(this),
                [typeof(RunningState)] = new RunningState(this),
                [typeof(SprintingState)] = new SprintingState(this),
            };
        }
    }
}
