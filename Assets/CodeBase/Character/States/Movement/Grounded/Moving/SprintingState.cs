using Assets.CodeBase.Character.Data.States.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class SprintingState : MovingState
    {
        private UnitSprintData _sprintData;

        private bool _keepSprinting = false;
        private float _startTime;

        public SprintingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _sprintData = _stateMachine.Player.Data.GroundedData.SprintData;
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _sprintData.SpeedModifier;
            _startTime = Time.time;
        }

        public override void Exit() {
            base.Exit();

            _keepSprinting = false;
        }

        public override void Update() {
            base.Update();

            if (_keepSprinting)
                return;

            if (Time.time > _startTime + _sprintData.SprintToRunTime)
                StopSprinting();
        }

        private void StopSprinting() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<IdlingState>();
            else _stateMachine.Enter<RunningState>();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.SprintPerformed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.SprintPerformed -= OnSprintPerformed;
        }

        private void OnSprintPerformed() {
            _keepSprinting = true;
        }

        protected override void OnMovementCancelled() {
            _stateMachine.Enter<HardStoppingState>();
        }
    }
}
