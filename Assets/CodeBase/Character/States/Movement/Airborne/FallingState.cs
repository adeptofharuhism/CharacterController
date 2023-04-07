using Assets.CodeBase.Character.Data.States.Airborne;
using System;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class FallingState : AirborneState
    {
        private UnitFallData _fallData;

        public FallingState(MovementStateMachine stateMachine) : base(stateMachine) {
            _fallData = _airborneData.FallData;
        }

        public override void Enter() {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVertivalVelocity();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        protected override void ResetSprintState() {
            
        }

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
