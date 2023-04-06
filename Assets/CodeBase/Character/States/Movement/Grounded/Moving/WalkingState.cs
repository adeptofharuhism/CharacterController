﻿using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _groundedData.WalkData.SpeedModifier;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }

        protected override void OnMovementCancelled() {
            _stateMachine.Enter<LightStoppingState>();
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<RunningState>();
        }
    }
}
