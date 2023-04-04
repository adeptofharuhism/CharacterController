using Assets.CodeBase.Character.Data.States;
using Assets.CodeBase.Character.States.Movement.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using Assets.CodeBase.Infrastructure;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Character.States.Movement
{
    public class MovementStateMachine : UnitStateMachine
    {
        private readonly Player.Player _player;
        private readonly UnitStateReusableData _reusableData;

        public Player.Player Player => _player;
        public UnitStateReusableData ReusableData => _reusableData;

        public MovementStateMachine(Player.Player player) {
            _player = player;

            _reusableData = new UnitStateReusableData();

            _states = new Dictionary<Type, IUnitExitableState>() {
                [typeof(IdlingState)] = new IdlingState(this),

                [typeof(WalkingState)] = new WalkingState(this),
                [typeof(RunningState)] = new RunningState(this),
                [typeof(SprintingState)] = new SprintingState(this),
                [typeof(DashingState)] = new DashingState(this),

                [typeof(LightStoppingState)] = new LightStoppingState(this),
                [typeof(MediumStoppingState)] = new MediumStoppingState(this),
                [typeof(HardStoppingState)] = new HardStoppingState(this),
            };
        }
    }
}
