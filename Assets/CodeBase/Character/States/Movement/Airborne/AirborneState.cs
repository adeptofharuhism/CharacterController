using Assets.CodeBase.Character.States.Movement.Grounded.Landing;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Airborne
{
    public class AirborneState : MovementState
    {
        public AirborneState(MovementStateConstructionData constructionData) : base(constructionData) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.AirborneParameterHash);

            ResetSprintState();
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.AirborneParameterHash);
        }

        protected override void OnContactWithGround(Collider collider) => 
            _stateMachine.Enter<LightLandingState>();

        protected virtual void ResetSprintState() => 
            _reusableData.IsSprinting = false;
    }
}
