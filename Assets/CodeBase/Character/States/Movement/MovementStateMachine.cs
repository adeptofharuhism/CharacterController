using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementStateMachine : UnitStateMachine
    {
        public MovementStateMachine() {
            _states = new Dictionary<Type, IUnitState>() {
                [typeof(IdlingState)] = new IdlingState(),
                [typeof(WalkingState)] = new WalkingState(),
                [typeof(RunningState)] = new RunningState(),
                [typeof(SprintingState)] = new SprintingState(),
            };
        }
    }
}
