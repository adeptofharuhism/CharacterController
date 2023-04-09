using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class DashingState : GroundedState
    {
        private UnitDashData _dashData;

        private float _startTime;
        private int _consecutiveDashesUsed;

        private bool _shouldKeepRotating;

        public DashingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _dashData = _stateMachine.Player.Data.GroundedData.DashData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.DashParameterHash);

            _stateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;
            _stateMachine.ReusableData.RotationData = _dashData.RotationData;
            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;

            Dash();
            UpdateConsecutiveDashes();

            _shouldKeepRotating = _stateMachine.ReusableData.MovementInput != Vector2.zero;

            _startTime = Time.time;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.DashParameterHash);

            SetBaseRotationData();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (!_shouldKeepRotating)
                return;

            RotateTowardsTargetRotation();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementPerformed += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementPerformed -= OnMovementStarted;
        }

        private void OnMovementStarted() {
            _shouldKeepRotating = true;
        }

        public override void OnAnimationTransitEvent() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<HardStoppingState>();
            else _stateMachine.Enter<SprintingState>();
        }

        protected override void OnMovementCancelled() { }
        protected override void OnDashStarted() { }

        private void Dash() {
            Vector3 dashDirection = _stateMachine.Player.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (_stateMachine.ReusableData.MovementInput != Vector2.zero) {
                UpdateTargetRotation(GetMovementDirection());
                dashDirection = GetTargetRotationDirection(_stateMachine.ReusableData.CurrentTargetRotation.y);
            }

            _stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }

        private void UpdateConsecutiveDashes() {
            if (!IsConsecutive())
                _consecutiveDashesUsed = 0;

            _consecutiveDashesUsed++;
            if (_consecutiveDashesUsed == _dashData.ConsecutiveDashesLimitAmount) {
                _consecutiveDashesUsed = 0;

                _stateMachine.Player.InputService.DisableDashFor(_dashData.DashLimitReachedCooldown);
            }
        }

        private bool IsConsecutive() =>
            Time.time < _startTime + _dashData.ConsecutiveTime;
    }
}
