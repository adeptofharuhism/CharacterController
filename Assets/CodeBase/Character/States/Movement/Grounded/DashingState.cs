using Assets.CodeBase.Character.Data.States.Grounded;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Character.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class DashingState : GroundedState
    {
        private UnitDashData _dashData;

        private float _startTime;
        private int _consecutiveDashesUsed;

        private bool _shouldKeepRotating;

        public DashingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {

            _dashData = _groundedData.DashData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.DashParameterHash);

            _reusableData.MovementSpeedModifier = _dashData.SpeedModifier;
            _reusableData.RotationData = _dashData.RotationData;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;

            Dash();
            UpdateConsecutiveDashes();

            _shouldKeepRotating = _reusableData.MovementInput != Vector2.zero;

            _startTime = Time.time;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.DashParameterHash);

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

            _inputService.MovementPerformed += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _inputService.MovementPerformed -= OnMovementStarted;
        }

        private void OnMovementStarted() {
            _shouldKeepRotating = true;
        }

        public override void OnAnimationTransitEvent() {
            if (_reusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<HardStoppingState>();
            else _stateMachine.Enter<SprintingState>();
        }

        protected override void OnMovementCancelled() { }
        protected override void OnDashStarted() { }

        private void Dash() {
            Vector3 dashDirection = _unitTransform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection);

            if (_reusableData.MovementInput != Vector2.zero) {
                UpdateTargetRotation(GetMovementDirection());
                dashDirection = GetTargetRotationDirection(_reusableData.CurrentTargetRotation.y);
            }

            _rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }

        private void UpdateConsecutiveDashes() {
            if (!IsConsecutive())
                _consecutiveDashesUsed = 0;

            _consecutiveDashesUsed++;
            if (_consecutiveDashesUsed == _dashData.ConsecutiveDashesLimitAmount) {
                _consecutiveDashesUsed = 0;

                _inputService.DisableDashFor(_dashData.DashLimitReachedCooldown);
            }
        }

        private bool IsConsecutive() =>
            Time.time < _startTime + _dashData.ConsecutiveTime;
    }
}
