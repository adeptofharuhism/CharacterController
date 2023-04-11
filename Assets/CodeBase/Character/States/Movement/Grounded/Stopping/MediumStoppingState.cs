using Assets.CodeBase.Utility.Colliders;
using UnityEngine;

namespace Assets.CodeBase.Character.States.Movement.Grounded.Stopping
{
    public class MediumStoppingState : StoppingState
    {
        public MediumStoppingState(MovementStateConstructionData constructionData, Transform unitTransform) : 
            base(constructionData, unitTransform) {
        }

        public override void Enter() {
            base.Enter();

            StartAnimation(_animationData.MediumStopParameterHash);

            _reusableData.MovementDecelerationForce =
                _groundedData.StopData.MediumDecelerationForce;
            _reusableData.CurrentJumpForce = _airborneData.JumpData.MediumForce;
        }

        public override void Exit() {
            base.Exit();

            StopAnimation(_animationData.MediumStopParameterHash);
        }
    }
}
