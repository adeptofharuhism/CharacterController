using Assets.CodeBase.Character.States.Movement.Airborne;
using Assets.CodeBase.Character.States.Movement.Grounded.Moving;
using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded
{
    public class GroundedState : MovementState
    {
        protected readonly SlopeData _slopeData;
        protected readonly Transform _unitTransform;

        public GroundedState(MovementStateConstructionData constructionData, Transform unitTransform) :
            base(constructionData) {

            _slopeData = _colliderUtility.SlopeData;
            _unitTransform = unitTransform;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.GroundedParameterHash);

            UpdateIsSprintingFlag();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.GroundedParameterHash);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            FloatCapsule();
        }

        protected override void AddInputActionsCallbacks() {
            base.AddInputActionsCallbacks();

            _inputService.MovementCancelled += OnMovementCancelled;
            _inputService.DashStarted += OnDashStarted;
            _inputService.JumpStarted += OnJumpStarted;
        }

        protected override void RemoveInputActionsCallbacks() {
            base.RemoveInputActionsCallbacks();

            _inputService.MovementCancelled -= OnMovementCancelled;
            _inputService.DashStarted -= OnDashStarted;
            _inputService.JumpStarted -= OnJumpStarted;
        }

        protected override void OnLostContactWithGround(Collider collider) {
            Vector3 capsuleColliderCenterInWorldSpace =
                _colliderUtility.CapsuleColliderData.Collider.bounds.center;

            if (IsThereGroundUnderneath())
                return;

            Ray downwardsRayFromCapsuleBottom =
                new Ray(
                    capsuleColliderCenterInWorldSpace - _colliderUtility.CapsuleColliderData.ColliderVerticalExtents,
                    Vector3.down);

            if (!Physics.Raycast(
                downwardsRayFromCapsuleBottom,
                out _,
                _groundedData.GroundToFallRayDistance,
                _layerData.GroundLayer,
                QueryTriggerInteraction.Ignore))
                OnFall();
        }

        private bool IsThereGroundUnderneath() {
            BoxCollider groundCheckCollider = _colliderUtility.TriggerColliderData.GroundCheckCollider;

            Vector3 groundColliderCenterInWorldspace =
                groundCheckCollider.bounds.center;

            Collider[] overlappedGroundCollider =
                Physics.OverlapBox(
                    groundColliderCenterInWorldspace,
                    _colliderUtility.TriggerColliderData.GroundCheckColliderExtents,
                    groundCheckCollider.transform.rotation,
                    _layerData.GroundLayer,
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
            if (_reusableData.IsSprinting) {
                _stateMachine.Enter<SprintingState>();
            } else if (_reusableData.IsWalking) {
                _stateMachine.Enter<WalkingState>();
            } else {
                _stateMachine.Enter<RunningState>();
            }
        }

        private void UpdateIsSprintingFlag() {
            if (!_reusableData.IsSprinting)
                return;

            if (_reusableData.MovementInput != Vector2.zero)
                return;

            _reusableData.IsSprinting = false;
        }

        private void FloatCapsule() {
            Vector3 capsuleColliderCenter = _colliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsFromCapsuleCenter = new(capsuleColliderCenter, Vector3.down);

            if (Physics.Raycast(
                downwardsFromCapsuleCenter,
                out RaycastHit hit,
                _slopeData.FloatRayDistance,
                _layerData.GroundLayer,
                QueryTriggerInteraction.Ignore)) {

                float groundAngle = Vector3.Angle(hit.normal, -downwardsFromCapsuleCenter.direction);

                float slopeSpeedModifier = SetSlopeModifierOnAngle(groundAngle);
                if (slopeSpeedModifier == 0f)
                    return;

                float distanceToFloatingPoint =
                    _colliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y *
                    _unitTransform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * _slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new(0f, amountToLift, 0f);

                _rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeModifierOnAngle(float groundAngle) {
            float slopeSpeedModifier = _groundedData.SlopeSpeedAngles.Evaluate(groundAngle);

            _reusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }
    }
}
