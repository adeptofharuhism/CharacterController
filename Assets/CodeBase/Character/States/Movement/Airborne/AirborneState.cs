using Assets.CodeBase.Character.States.Movement.Grounded.Landing;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class AirborneState : MovementState
    {
        public AirborneState(MovementStateMachine stateMachine) : base(stateMachine) {
        }

        public override void Enter() {
            base.Enter();

            ResetSprintState();
        }

        protected override void OnContactWithGround(Collider collider) => 
            _stateMachine.Enter<LightLandingState>();

        protected virtual void ResetSprintState() => 
            _stateMachine.ReusableData.IsSprinting = false;
    }
}
