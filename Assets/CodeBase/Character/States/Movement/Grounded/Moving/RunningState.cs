using System;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class RunningState : MovingState
    {
        private float _startTime;

        public RunningState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _groundedData.RunData.SpeedModifier;

            _startTime = Time.time;
        }

        public override void Update() {
            base.Update();

            if (!_stateMachine.ReusableData.IsWalking)
                return;

            if (Time.time > _startTime + _groundedData.RunData.SpeedModifier)
                StopRunning();
        }

        private void StopRunning() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<IdlingState>();
            else _stateMachine.Enter<WalkingState>();
        }

        protected override void WalkToggle() {
            base.WalkToggle();

            _stateMachine.Enter<WalkingState>();
        }
    }
}
