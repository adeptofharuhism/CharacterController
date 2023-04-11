using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Moving
{
    public class MovingState : GroundedState
    {
        public MovingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.MovingParameterHash);
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.MovingParameterHash);
        }
    }
}
