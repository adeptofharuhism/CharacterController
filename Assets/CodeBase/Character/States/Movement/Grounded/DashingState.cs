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

        public DashingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _dashData = _stateMachine.Player.Data.GroundedData.DashData;
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;

            AddForceOnTransitionFromIdleState();
            UpdateConsecutiveDashes();

            _startTime = Time.time;
        }

        public override void OnAnimationTransitEvent() {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
                _stateMachine.Enter<HardStoppingState>();
            else _stateMachine.Enter<SprintingState>();
        }

        protected override void OnMovementCancelled() { }
        protected override void OnDashStarted() { }

        private void AddForceOnTransitionFromIdleState() {
            if (_stateMachine.ReusableData.MovementInput != Vector2.zero)
                return;

            Vector3 characterRotationDirection = _stateMachine.Player.transform.forward;

            characterRotationDirection.y = 0f;

            _stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
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
