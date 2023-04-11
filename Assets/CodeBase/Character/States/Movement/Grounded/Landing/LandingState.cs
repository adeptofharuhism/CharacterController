using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Landing
{
    public class LandingState : GroundedState
    {
        public LandingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        protected override void OnMovementCancelled() {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.LandingParameterHash);
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.LandingParameterHash);
        }
    }
}
