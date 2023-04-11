using Assets.CodeBase.Character.Data.States.Airborne;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class JumpingState : AirborneState
    {
        private readonly Transform _unitTransform;
        private readonly UnitJumpData _jumpData;

        private bool _shouldKeepRotating;
        private bool _canStartFalling;

        public JumpingState(MovementStateConstructionData constructionData, Transform unitTransform) : base(constructionData) {
            _unitTransform = unitTransform;
            _jumpData = _airborneData.JumpData;
        }

        public override void Enter() {
            base.Enter();

            _reusableData.MovementSpeedModifier = 0;
            _reusableData.RotationData = _jumpData.RotationData;
            _reusableData.MovementDecelerationForce = _jumpData.DecelerationForce;

            _shouldKeepRotating = _reusableData.MovementInput != Vector2.zero;

            Jump();
        }

        public override void Exit() {
            base.Exit();

            SetBaseRotationData();
            _canStartFalling = false;
        }

        public override void Update() {
            base.Update();

            if (!_canStartFalling && IsMovingUp())
                _canStartFalling = true;

            if (!_canStartFalling || GetPlayerVerticalVelocity().y > 0)
                return;

            _stateMachine.Enter<FallingState>();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            if (_shouldKeepRotating)
                RotateTowardsTargetRotation();

            if (IsMovingUp())
                DecelerateVertically();
        }

        private void Jump() {
            Vector3 jumpForce = _reusableData.CurrentJumpForce;

            Vector3 jumpDirection = _unitTransform.forward;

            if (_shouldKeepRotating) {
                UpdateTargetRotation(GetMovementDirection());
                jumpDirection = GetTargetRotationDirection(_reusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            Vector3 capsuleColliderCenterInWorldSpace =
                _colliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(
                downwardsRayFromCapsuleCenter,
                out RaycastHit hit,
                _jumpData.JumpToGroundRayDistance,
                _layerData.GroundLayer,
                QueryTriggerInteraction.Ignore)) {

                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                if (IsMovingUp()) {
                    float forceModifier = _jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);
                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown()) {
                    float forceModifier = _jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);
                    jumpForce.y *= forceModifier;
                }
            }

            ResetVelocity();

            _rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        protected override void ResetSprintState() { }
    }
}
