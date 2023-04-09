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

            StartAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);

            ResetSprintState();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);
        }

        protected override void OnContactWithGround(Collider collider) => 
            _stateMachine.Enter<LightLandingState>();

        protected virtual void ResetSprintState() => 
            _stateMachine.ReusableData.IsSprinting = false;
    }
}
