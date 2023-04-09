using Assets.CodeBase.Character.States.Movement.Airborne;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class GroundedState : MovementState
    {
        private SlopeData _slopeData;

        public GroundedState(MovementStateMachine stateMachine) : base(stateMachine) {
            _slopeData = stateMachine.Player.ColliderUtility.SlopeData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.GroundedParameterHash);

            UpdateIsSprintingFlag();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.GroundedParameterHash);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            FloatCapsule();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled += OnMovementCancelled;
            _stateMachine.Player.InputService.DashStarted += OnDashStarted;
            _stateMachine.Player.InputService.JumpStarted += OnJumpStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.InputService.MovementCancelled -= OnMovementCancelled;
            _stateMachine.Player.InputService.DashStarted -= OnDashStarted;
            _stateMachine.Player.InputService.JumpStarted -= OnJumpStarted;
        }

        protected override void OnLostContactWithGround(Collider collider) {
            Vector3 capsuleColliderCenterInWorldSpace =
                _stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            if (IsThereGroundUnderneath())
                return;

            Ray downwardsRayFromCapsuleBottom =
                new Ray(
                    capsuleColliderCenterInWorldSpace -
                    _stateMachine.
                        Player.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents,
                    Vector3.down);

            if (!Physics.Raycast(
                downwardsRayFromCapsuleBottom,
                out _,
                _groundedData.GroundToFallRayDistance,
                _stateMachine.Player.LayerData.GroundLayer,
                QueryTriggerInteraction.Ignore))
                OnFall();
        }

        private bool IsThereGroundUnderneath() {
            BoxCollider groundCheckCollider = _stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckCollider;

            Vector3 groundColliderCenterInWorldspace =
                groundCheckCollider.bounds.center;

            Collider[] overlappedGroundCollider =
                Physics.OverlapBox(
                    groundColliderCenterInWorldspace,
                    _stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckColliderExtents,
                    groundCheckCollider.transform.rotation,
                    _stateMachine.Player.LayerData.GroundLayer,
                    QueryTriggerInteraction.Ignore);

            return overlappedGroundCollider.Length > 0;
        }

        protected virtual void OnFall() =>
            _stateMachine.Enter<FallingState>();

        protected virtual void OnJumpStarted() =>
            _stateMachine.Enter<JumpingState>();

        protected virtual void OnMovementCancelled() =>
            _stateMachine.Enter<IdlingState>();

        protected virtual void OnDashStarted() =>
            _stateMachine.Enter<DashingState>();

        protected virtual void OnMove() {
            if (_stateMachine.ReusableData.IsSprinting) {
                _stateMachine.Enter<SprintingState>();
            } else if (_stateMachine.ReusableData.IsWalking) {
                _stateMachine.Enter<WalkingState>();
            } else {
                _stateMachine.Enter<RunningState>();
            }
        }

        private void UpdateIsSprintingFlag() {
            if (!_stateMachine.ReusableData.IsSprinting)
                return;

            if (_stateMachine.ReusableData.MovementInput != Vector2.zero)
                return;

            _stateMachine.ReusableData.IsSprinting = false;
        }

        private void FloatCapsule() {
            Vector3 capsuleColliderCenter = _stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsFromCapsuleCenter = new(capsuleColliderCenter, Vector3.down);

            if (Physics.Raycast(
                downwardsFromCapsuleCenter,
                out RaycastHit hit,
                _slopeData.FloatRayDistance,
                _stateMachine.Player.LayerData.GroundLayer,
                QueryTriggerInteraction.Ignore)) {

                float groundAngle = Vector3.Angle(hit.normal, -downwardsFromCapsuleCenter.direction);

                float slopeSpeedModifier = SetSlopeModifierOnAngle(groundAngle);
                if (slopeSpeedModifier == 0f)
                    return;

                float distanceToFloatingPoint =
                    _stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y *
                    _stateMachine.Player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * _slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new(0f, amountToLift, 0f);

                _stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeModifierOnAngle(float groundAngle) {
            float slopeSpeedModifier = _stateMachine.Player.Data.GroundedData.SlopeSpeedAngles.Evaluate(groundAngle);

            _stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }
    }
}
