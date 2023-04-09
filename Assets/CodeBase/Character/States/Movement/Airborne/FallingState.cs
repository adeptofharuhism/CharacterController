using Assets.CodeBase.Character.Data.States.Airborne;
using Assets.CodeBase.Character.States.Movement.Grounded.Landing;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class FallingState : AirborneState
    {
        private UnitFallData _fallData;

        private Vector3 _playerPositionOnEnter;

        public FallingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _fallData = _airborneData.FallData;
        }

        public override void Enter() {
            base.Enter();

            _playerPositionOnEnter = _stateMachine.Player.transform.position;

            _stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVertivalVelocity();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        protected override void OnContactWithGround(Collider collider) {
            float fallDistance = _playerPositionOnEnter.y - _stateMachine.Player.transform.position.y;

            if (fallDistance < _fallData.MinimalDistanceAsHardFall) {
                _stateMachine.Enter<LightLandingState>();
                return;
            }

            if (ShouldHardLand())
                _stateMachine.Enter<HardLandingState>();
            else _stateMachine.Enter<RollingState>();
        }

        private bool ShouldHardLand() => 
            _stateMachine.ReusableData.IsWalking && !_stateMachine.ReusableData.IsSprinting 
            || _stateMachine.ReusableData.MovementInput == Vector2.zero;

        protected override void ResetSprintState() { }

        private void LimitVerticalVelocity() {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            if (playerVerticalVelocity.y >= -_fallData.FallSpeedLimit)
                return;

            Vector3 limitedVelocity =
                new Vector3(0f, -_fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            _stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
        }
    }
}
