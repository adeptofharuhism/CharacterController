using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.States.Movement.Grounded.Landing;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class FallingState : AirborneState
    {
        private readonly Transform _unitTransform;
        private readonly UnitFallData _fallData;

        private Vector3 _playerPositionOnEnter;

        public FallingState(MovementStateConstructionData constructionData, Transform unitTransform) : base(constructionData) {
            _unitTransform = unitTransform;
            _fallData = _airborneData.FallData;
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.FallParameterHash);

            _playerPositionOnEnter = _unitTransform.position;

            _reusableData.MovementSpeedModifier = 0f;

            ResetVertivalVelocity();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.FallParameterHash);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        protected override void OnContactWithGround(Collider collider) {
            float fallDistance = _playerPositionOnEnter.y - _unitTransform.position.y;

            if (fallDistance < _fallData.MinimalDistanceAsHardFall) {
                _stateMachine.Enter<LightLandingState>();
                return;
            }

            if (ShouldHardLand())
                _stateMachine.Enter<HardLandingState>();
            else _stateMachine.Enter<RollingState>();
        }

        private bool ShouldHardLand() => 
            _reusableData.IsWalking && !_reusableData.IsSprinting 
            || _reusableData.MovementInput == Vector2.zero;

        protected override void ResetSprintState() { }

        private void LimitVerticalVelocity() {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            if (playerVerticalVelocity.y >= -_fallData.FallSpeedLimit)
                return;

            Vector3 limitedVelocity =
                new Vector3(0f, -_fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            _rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
        }
    }
}
