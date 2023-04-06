﻿namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class LightStoppingState : StoppingState
    {
        public LightStoppingState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementDecelerationForce =
                _stateMachine.Player.Data.GroundedData.StopData.LightDecelerationForce;

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }
    }
}
